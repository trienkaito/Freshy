using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;

public class ProductTypeId : ValueObject
{
    public Guid Value { get; set; }

    public ProductTypeId(Guid value)
    {
        Value = value;
    }

    public static ProductTypeId CreateUnique()
    {
        return new ProductTypeId(Guid.NewGuid());
    }

    public static ProductTypeId Create(Guid value)
    {
        return new ProductTypeId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}