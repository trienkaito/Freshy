using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Abstractions.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails;

public record GetOrderDetailsQuery
(
    Guid OrderId
) : IQuery<QueryResult<OrderDetailsResult>>;

public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, QueryResult<OrderDetailsResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IShippingRepository _shippingRepository;
    private readonly IVoucherRepository _voucherRepository;
    private readonly ICustomerProfileRepository _customerProfileRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IProductUnitRepository _productUnitRepository;

    public GetOrderDetailsQueryHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IVoucherRepository voucherRepository,
        ICustomerProfileRepository customerProfileRepository,
        IShippingRepository shippingRepository,
        IOrderItemRepository orderItemRepository,
        IProductTypeRepository productTypeRepository,
        IProductUnitRepository productUnitRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _voucherRepository = voucherRepository;
        _customerProfileRepository = customerProfileRepository;
        _shippingRepository = shippingRepository;
        _orderItemRepository = orderItemRepository;
        _productTypeRepository = productTypeRepository;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<QueryResult<OrderDetailsResult>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(OrderDetailId.Create(request.OrderId));

            if (order is not null)
            {
                var customer = await _customerProfileRepository.GetByIdAsync(order.CustomerId, profile => new
                {
                    profile.Id,
                    profile.Name,
                    profile.Avatar
                });

                var usedVoucher = await _voucherRepository.GetByIdAsync(order?.VoucherId);

                var usedShipping = await _shippingRepository.GetByIdAsync(order?.ShippingId, shipping => new
                {
                    shipping.Name,
                    shipping.ShippingPrice
                });

                var orderItems = await _orderItemRepository.GetOrderItemsByOrderDetailsIdAsync(order!.Id);

                var data = new OrderDetailsResult(
                    order.Id.Value,
                    new OrderCustomerResult(
                        customer!.Id.Value,
                        customer.Name,
                        customer.Avatar //UPDATE DEFAULT VALUE IN DOMAIN
                    ),
                    order.OrderAddress is null ? OrderPlaceStatus.OFFLINE : order.OrderAddress,
                    order.CreatedDate,
                    new OrderStatusResult(
                        order.OrderStatus.ToString()
                    ),
                    order.ProductsAmount,
                    new PaymentTypeResult(
                        order.PaymentType.ToString()
                    ),
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
                    orderItems.Select(item =>
                    {
                        var product = _productRepository.GetByIdAsync(item.ProductId, product => new
                        {
                            product.Name,
                            product.FeatureImage,
                            product.TypeId
                        }).Result;

                        var type = _productTypeRepository.GetByIdAsync(product!.TypeId).Result;

                        var unit = _productUnitRepository.GetByIdAsync(item.ProductUnitId, unit => new
                        {
                            unit.UnitType,
                            unit.SellPrice
                        }).Result;

                        return new OrderItemResult(
                            item.ProductId.Value,
                            item.Id.Value,
                            product!.Name,
                            type!.Name,
                            unit!.UnitType,
                            unit.SellPrice,
                            product.FeatureImage,
                            item.BoughtQuantity,
                            item.TotalPrice
                        );
                    }).ToList(),
                    order.PaidAmount
                );
                return new QueryResult<OrderDetailsResult>(data);
            }
            return new QueryResult<OrderDetailsResult>(HttpStatusCode.InternalServerError, "ORDER NULL");
        }
        catch (Exception e)
        {
            return new QueryResult<OrderDetailsResult>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}