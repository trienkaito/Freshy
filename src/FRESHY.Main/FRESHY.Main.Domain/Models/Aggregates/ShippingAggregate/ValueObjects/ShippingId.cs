using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;

public class ShippingId : ValueObject
{
    public Guid Value { get; set; }

    public ShippingId(Guid value)
    {
        Value = value;
    }

    public static ShippingId CreateUnique()
    {
        return new ShippingId(Guid.NewGuid());
    }

    public static ShippingId Create(Guid value)
    {
        return new ShippingId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}