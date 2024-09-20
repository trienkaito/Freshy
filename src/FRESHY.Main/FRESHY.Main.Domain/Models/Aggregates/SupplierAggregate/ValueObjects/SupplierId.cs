using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;

public class SupplierId : ValueObject
{
    public Guid Value { get; set; }

    public SupplierId(Guid value)
    {
        Value = value;
    }

    public static SupplierId CreateUnique()
    {
        return new SupplierId(Guid.NewGuid());
    }

    public static SupplierId Create(Guid value)
    {
        return new SupplierId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}