using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllCustomerOrders.Results;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Abstractions.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllCustomerOrders;

public record GetAllCustomerOrdersQuery
(
    int PageNumber,
    int PageSize,
    Guid CustomerId
) : IQuery<PageQueryResult<IEnumerable<AllCustomerOrdersResult>>>;

public class GetAllCustomerOrdersQueryHandler : IQueryHandler<GetAllCustomerOrdersQuery, PageQueryResult<IEnumerable<AllCustomerOrdersResult>>>
{
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;
    private readonly IShippingRepository _shippingRepository;

    public GetAllCustomerOrdersQueryHandler(
        IOrderRepository orderRepository,
        IVoucherRepository voucherRepository,
        IShippingRepository shippingRepository,
        ICustomerProfileRepository customerProfileRepository)
    {
        _orderRepository = orderRepository;
        _voucherRepository = voucherRepository;
        _shippingRepository = shippingRepository;
        _customerProfileRepository = customerProfileRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllCustomerOrdersResult>>> Handle(GetAllCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _customerProfileRepository.GetByIdAsync(CustomerId.Create(request.CustomerId), profile => new
            {
                profile.Id,
                profile.Name
            });

            if (customer is not null)
            {
                var ordersOfCustomer = await _orderRepository.GetOrdersPagingOfACustomerByCustomerIdAsync(customer.Id, request.PageNumber, request.PageSize, order => new
                {
                    order.Id,
                    order.ShippingId,
                    order.CreatedDate,
                    order.VoucherId,
                    order.OrderStatus,
                    order.PaymentType,
                    order.PaidAmount,
                    order.OrderAddress
                });

                var allOrdersOfCustomer = await _orderRepository.GetOrdersOfACustomerByCustomerIdAsync(customer.Id, order => new
                {
                    order.Id
                });

                int totalPage = (int)Math.Ceiling((double)allOrdersOfCustomer.Count() / request.PageSize);

                var data = ordersOfCustomer.Select(order =>
                {
                    var usedVoucher = _voucherRepository.GetByIdAsync(order?.VoucherId).Result;

                    var usedShipping = _shippingRepository.GetByIdAsync(order?.ShippingId, shipping => new
                    {
                        shipping.Name,
                        shipping.ShippingPrice
                    }).Result;

                    return new AllCustomerOrdersResult(
                        order.Id.Value,
                        customer.Id.Value,
                        customer.Name,
                        order!.OrderAddress is null ? OrderPlaceStatus.OFFLINE : order.OrderAddress,
                        order.CreatedDate,
                        new OrderStatusResult(order.OrderStatus.ToString()),
                        new PaymentTypeResult(order.PaymentType.ToString()),
                        usedShipping is not null ? new OrderShippingResult(
                            usedShipping.Name,
                            usedShipping.ShippingPrice) : new OrderShippingResult(
                                ShippingStatus.INSTORE,
                                0),
                        usedVoucher is not null ? new OrderVoucherResult(
                            new VoucherCodeResult(usedVoucher.VoucherCode.Value),
                            usedVoucher.DiscountValue) : new OrderVoucherResult(
                                new VoucherCodeResult(VoucherStatus.VOUCHER_NOT_USED),
                                0),
                        order.PaidAmount
                    );
                }).ToList();

                return new PageQueryResult<IEnumerable<AllCustomerOrdersResult>>(data, request.PageNumber, request.PageSize, totalPage);
            }
            return new PageQueryResult<IEnumerable<AllCustomerOrdersResult>>(request.PageNumber, request.PageSize, HttpStatusCode.NotFound, Error.NOT_FOUND);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllCustomerOrdersResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}