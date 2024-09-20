using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using System.Linq.Expressions;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IOrderRepository : IRepository<OrderDetail, OrderDetailId>
{
    Task<IEnumerable<TResult?>> GetOrdersOfACustomerByCustomerIdAsync<TResult>(CustomerId customerId, Expression<Func<OrderDetail, TResult>> selector);

    Task<IEnumerable<TResult?>> GetOrdersPagingOfACustomerByCustomerIdAsync<TResult>(CustomerId customerId, int pageNumber, int pageSize, Expression<Func<OrderDetail, TResult>> selector);
}