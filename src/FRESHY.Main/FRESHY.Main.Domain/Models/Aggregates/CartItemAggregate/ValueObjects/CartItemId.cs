using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;

public class CartItemId : ValueObject
{
    public Guid Value { get; set; }

    public CartItemId(Guid value)
    {
        Value = value;
    }

    public static CartItemId CreateUnique()
    {
        return new CartItemId(Guid.NewGuid());
    }

    public static CartItemId Create(Guid value)
    {
        return new CartItemId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}