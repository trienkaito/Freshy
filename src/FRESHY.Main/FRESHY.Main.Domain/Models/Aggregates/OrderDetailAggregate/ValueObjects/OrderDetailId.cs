using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;

public class OrderDetailId : ValueObject
{
    public Guid Value { get; set; }

    public OrderDetailId(Guid value)
    {
        Value = value;
    }

    public static OrderDetailId CreateUnique()
    {
        return new OrderDetailId(Guid.NewGuid());
    }

    public static OrderDetailId Create(Guid value)
    {
        return new OrderDetailId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}