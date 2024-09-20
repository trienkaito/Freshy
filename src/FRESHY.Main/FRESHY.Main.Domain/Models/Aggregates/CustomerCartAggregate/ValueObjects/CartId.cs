using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

public class CartId : ValueObject
{
    public Guid Value { get; set; }

    public CartId(Guid value)
    {
        Value = value;
    }

    public static CartId CreateUnique()
    {
        return new CartId(Guid.NewGuid());
    }

    public static CartId Create(Guid value)
    {
        return new CartId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}