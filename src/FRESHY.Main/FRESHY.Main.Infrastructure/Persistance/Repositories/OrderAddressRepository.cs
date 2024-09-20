using System.Linq.Expressions;
using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class OrderAddressRepository : Repository<OrderAddress, OrderAddressId, FreshyDbContext>, IOrderAddressRepository
{
    public OrderAddressRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TResult>?> GetOrderAddressesOfCustomer<TResult>(CustomerId customerId, Expression<Func<OrderAddress, TResult>> selector)
    {
        return await _entitySet
        .AsNoTracking()
        .Where(address => address.CustomerId.Equals(customerId))
        .Select(selector)
        .ToListAsync();
    }
}