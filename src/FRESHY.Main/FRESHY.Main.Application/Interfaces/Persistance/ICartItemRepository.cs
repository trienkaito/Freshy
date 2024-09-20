using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface ICartItemRepository : IRepository<CartItem, CartItemId>
{
    Task<IEnumerable<CartItem>?> GetCartItemByCartIdAsync(CartId cartId);
    
    Task<CartItem?> GetByCartProductAndUnitIdAsync(CartId cartId, ProductId productId, ProductUnitId productUnitId);

    Task<IEnumerable<CartItem>?> GetCartItemByProductUnitId(ProductUnitId productUnitId);
}