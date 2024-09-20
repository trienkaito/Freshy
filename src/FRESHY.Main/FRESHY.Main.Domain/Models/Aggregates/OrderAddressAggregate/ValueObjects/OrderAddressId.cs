using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderAddressAggregate.ValueObjects;

public class OrderAddressId : ValueObject
{
    public Guid Value { get; set; }

    public OrderAddressId(Guid value)
    {
        Value = value;
    }

    public static OrderAddressId CreateUnique()
    {
        return new OrderAddressId(Guid.NewGuid());
    }

    public static OrderAddressId Create(Guid value)
    {
        return new OrderAddressId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}