using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;

public class JobPositionId : ValueObject
{
    public Guid Value { get; set; }

    public JobPositionId(Guid value)
    {
        Value = value;
    }

    public static JobPositionId CreateUnique()
    {
        return new JobPositionId(Guid.NewGuid());
    }

    public static JobPositionId Create(Guid value)
    {
        return new JobPositionId(value);
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}