using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class OrderRepository : Repository<OrderDetail, OrderDetailId, FreshyDbContext>, IOrderRepository
{
    public OrderRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TResult?>> GetOrdersOfACustomerByCustomerIdAsync<TResult>(CustomerId customerId, Expression<Func<OrderDetail, TResult>> selector)
    {
        return await _entitySet
        .Where(order => order.CustomerId.Equals(customerId))
        .Select(selector)
        .ToListAsync();
    }

    public async Task<IEnumerable<TResult?>> GetOrdersPagingOfACustomerByCustomerIdAsync<TResult>(CustomerId customerId, int pageNumber, int pageSize, Expression<Func<OrderDetail, TResult>> selector)
    {
        return await _entitySet
        .AsNoTracking()
        .Where(order => order.CustomerId.Equals(customerId))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(selector)
        .ToListAsync();
    }
}