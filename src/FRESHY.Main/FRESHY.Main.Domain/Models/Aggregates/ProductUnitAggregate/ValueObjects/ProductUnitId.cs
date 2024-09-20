using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

public class ProductUnitId : ValueObject
{
    public Guid Value { get; set; }

    public ProductUnitId(Guid value)
    {
        Value = value;
    }

    public static ProductUnitId CreateUnique()
    {
        return new ProductUnitId(Guid.NewGuid());
    }

    public static ProductUnitId Create(Guid value)
    {
        return new ProductUnitId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}