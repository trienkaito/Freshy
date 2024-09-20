using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemsOfCustomerQuery.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Linq;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.CartItemAbstractions.Queries.GetCartItemOfCustomerCartQuery;

public record GetCartItemsOfCustomerCartQuery
(
    Guid CustomerId
) : IQuery<QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>>;

public class GetCartItemsOfCustomerQueryHandler : IQueryHandler<GetCartItemsOfCustomerCartQuery, QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductUnitRepository _productUnitRepository;
        
    public GetCartItemsOfCustomerQueryHandler(
        ICartItemRepository cartItemRepository,
        IProductRepository productRepository,
        IProductUnitRepository productUnitRepository,
        ICartRepository cartRepository)
    {
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _cartRepository = cartRepository;
    }

    public async Task<QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>> Handle(GetCartItemsOfCustomerCartQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await _cartRepository.GetCartByCustomerId(CustomerId.Create(request.CustomerId));
            var items = await _cartItemRepository.GetCartItemByCartIdAsync(cart.Id);

            if (items is not null)
            {
                var data = items.Select(item =>
                {
                    var product = _productRepository.GetByIdAsync(item.ProductId, product => new { product.Name }).Result;
                    var unit = _productUnitRepository.GetByIdAsync(item.ProductUnitId, unit => new
                    {
                        unit.UnitFeatureImage,
                        unit.UnitType,
                        unit.UnitValue,
                        unit.SellPrice
                    }).Result;
                   
                    return new CartItemsOfCustomerCartResult(
                        item.Id.Value,
                        item.ProductId.Value,
                        item.ProductUnitId.Value,
                        product!.Name,
                        new ProductUnitForCustomerResult(
                            unit!.UnitFeatureImage,
                            unit!.UnitType,
                            unit.UnitValue,
                            unit.SellPrice
                        ),
                        item.BoughtQuantity,
                        item.TotalPrice
                    );
                }).ToList();
                return new QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>(data);
            }
            return new QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>(HttpStatusCode.NotFound, Error.ITEM_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<CartItemsOfCustomerCartResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}