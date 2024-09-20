using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;

public class VoucherId : ValueObject
{
    public Guid Value { get; set; }

    public VoucherId(Guid value)
    {
        Value = value;
    }

    public static VoucherId CreateUnique()
    {
        return new VoucherId(Guid.NewGuid());
    }

    public static VoucherId Create(Guid value)
    {
        return new VoucherId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}