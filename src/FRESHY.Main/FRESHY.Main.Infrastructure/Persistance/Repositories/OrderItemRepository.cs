using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class OrderItemRepository : Repository<OrderItem, OrderItemId, FreshyDbContext>, IOrderItemRepository
{
    public OrderItemRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderDetailsIdAsync(OrderDetailId orderId)
    {
        return await _entitySet.Where(item => item.OrderDetailId.Equals(orderId)).ToListAsync();
    }
}