using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface ICartRepository : IRepository<Cart, CartId>
{
    Task<Cart> GetCartByCustomerId(CustomerId customerId);
}