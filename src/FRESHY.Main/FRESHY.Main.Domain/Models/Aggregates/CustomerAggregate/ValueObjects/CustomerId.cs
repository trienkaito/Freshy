using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

public class CustomerId : ValueObject
{
    public Guid Value { get; set; }

    public CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreateUnique()
    {
        return new CustomerId(Guid.NewGuid());
    }

    public static CustomerId Create(Guid value)
    {
        return new CustomerId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}