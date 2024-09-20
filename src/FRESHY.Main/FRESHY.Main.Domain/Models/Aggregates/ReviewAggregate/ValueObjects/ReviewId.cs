using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

public class ReviewId : ValueObject
{
    public Guid Value { get; set; }

    public ReviewId(Guid value)
    {
        Value = value;
    }

    public static ReviewId CreateUnique()
    {
        return new ReviewId(Guid.NewGuid());
    }

    public static ReviewId Create(Guid value)
    {
        return new ReviewId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}