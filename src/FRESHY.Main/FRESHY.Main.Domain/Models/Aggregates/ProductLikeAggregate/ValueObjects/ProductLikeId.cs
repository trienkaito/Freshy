using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

public class ProductLikeId : ValueObject
{
    public Guid Value { get; set; }

    public ProductLikeId(Guid value)
    {
        Value = value;
    }

    public static ProductLikeId CreateUnique()
    {
        return new ProductLikeId(Guid.NewGuid());
    }

    public static ProductLikeId Create(Guid value)
    {
        return new ProductLikeId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}