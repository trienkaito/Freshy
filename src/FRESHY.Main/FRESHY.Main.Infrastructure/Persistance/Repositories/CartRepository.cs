using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class CartRepository : Repository<Cart, CartId, FreshyDbContext>, ICartRepository
{
    public CartRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<Cart> GetCartByCustomerId(CustomerId customerId)
    {
        var cart = await _entitySet.FirstOrDefaultAsync(cart => cart.CustomerId.Equals(customerId));
        return cart!;
    }
}