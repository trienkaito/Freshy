using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class CartItemRepository : Repository<CartItem, CartItemId, FreshyDbContext>, ICartItemRepository
{
    public CartItemRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<CartItem?> GetByCartProductAndUnitIdAsync(CartId cartId, ProductId productId, ProductUnitId productUnitId)
    {
        return await _entitySet.FirstOrDefaultAsync(item =>
            item.CartId.Equals(cartId) &&
            item.ProductId.Equals(productId) &&
            item.ProductUnitId.Equals(productUnitId));
    }

    public async Task<IEnumerable<CartItem>?> GetCartItemByCartIdAsync(CartId cartId)
    {
        return await _entitySet.Where(cart => cart.CartId.Equals(cartId)).ToListAsync();
    }

    public async Task<IEnumerable<CartItem>?> GetCartItemByProductUnitId(ProductUnitId productUnitId)
    {
        return await _entitySet.Where(cart => cart.ProductUnitId.Equals(productUnitId)).ToListAsync();
    }
}