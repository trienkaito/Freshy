using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate.ValueObjects;

public class OrderItemId : ValueObject
{
    public Guid Value { get; set; }

    public OrderItemId(Guid value)
    {
        Value = value;
    }

    public static OrderItemId CreateUnique()
    {
        return new OrderItemId(Guid.NewGuid());
    }

    public static OrderItemId Create(Guid value)
    {
        return new OrderItemId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}