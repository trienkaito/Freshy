using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderItemsOfOrder;

public record GetOrderItemsOfOrderQuery
(
    Guid OrderId
) : IQuery<QueryResult<IEnumerable<OrderItemResult>>>;

public class GetOrderItemsOfOrderQueryHandler : IQueryHandler<GetOrderItemsOfOrderQuery, QueryResult<IEnumerable<OrderItemResult>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
    private readonly IProductTypeRepository _productTypeRepository;

    public GetOrderItemsOfOrderQueryHandler(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        IProductTypeRepository productTypeRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _productTypeRepository = productTypeRepository;
    }

    public async Task<QueryResult<IEnumerable<OrderItemResult>>> Handle(GetOrderItemsOfOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(OrderDetailId.Create(request.OrderId));

            if (order is not null)
            {
                var items = await _orderItemRepository.GetOrderItemsByOrderDetailsIdAsync(order.Id);

                if (items is not null)
                {
                    var data = items.Select(item =>
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
                    }).ToList();
                }
                return new QueryResult<IEnumerable<OrderItemResult>>(HttpStatusCode.NoContent, Error.NO_CONTENT);
            }
            return new QueryResult<IEnumerable<OrderItemResult>>(HttpStatusCode.NotFound, Error.NOT_FOUND);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<OrderItemResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}