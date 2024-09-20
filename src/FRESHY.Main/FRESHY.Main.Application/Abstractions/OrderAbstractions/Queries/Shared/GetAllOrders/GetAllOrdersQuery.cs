using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllOrders.Results;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllOrders;

public record GetAllOrdersQuery
(
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllOrdersResult>>>;

public class GetAllOrderDetailsQueryHandler : IQueryHandler<GetAllOrdersQuery, PageQueryResult<IEnumerable<AllOrdersResult>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrderDetailsQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllOrdersResult>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetByPagingAsync(request.PageNumber, request.PageSize, order => new
            {
                order.CustomerId,
                order.Id,
                order.OrderAddress,
                order.CreatedDate,
                order.OrderStatus,
                order.PaymentType,
                order.PaidAmount
            });

            var allOrders = await _orderRepository.GetAllAsync(order => new
            {
                order.Id
            });

            int totalPage = (int)Math.Ceiling((double)allOrders.Count() / request.PageSize);

            var data = orders.Select(order => new AllOrdersResult(
                order.CustomerId.Value,
                order.Id.Value,
                order.OrderAddress,
                order.CreatedDate,
                new OrderStatusResult(
                order.OrderStatus.ToString()
                ),
                new PaymentTypeResult(
                    order.PaymentType.ToString()
                ),
                order.PaidAmount
            )).ToList();

            return new PageQueryResult<IEnumerable<AllOrdersResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllOrdersResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}