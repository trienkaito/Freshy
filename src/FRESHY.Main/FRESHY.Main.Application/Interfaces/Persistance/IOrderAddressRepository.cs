using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate.ValueObjects;
using System.Linq.Expressions;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IOrderAddressRepository : IRepository<OrderAddress, OrderAddressId>
{
    Task<IEnumerable<TResult>?> GetOrderAddressesOfCustomer<TResult>(CustomerId customerId, Expression<Func<OrderAddress, TResult>> selector);
}