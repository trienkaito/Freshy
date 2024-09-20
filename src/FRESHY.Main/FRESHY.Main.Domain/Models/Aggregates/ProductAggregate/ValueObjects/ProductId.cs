using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;

public class ProductId : ValueObject
{
    public Guid Value { get; set; }

    public ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
        return new ProductId(Guid.NewGuid());
    }

    public static ProductId Create(Guid value)
    {
        return new ProductId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}