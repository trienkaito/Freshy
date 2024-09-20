using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IOrderItemRepository : IRepository<OrderItem, OrderItemId>
{
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderDetailsIdAsync(OrderDetailId orderId);
}