using FRESHY.Common.Domain.Common.Models;

namespace FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;

public class EmployeeId : ValueObject
{
    public Guid Value { get; set; }

    public EmployeeId(Guid value)
    {
        Value = value;
    }

    public static EmployeeId Create(Guid value)
    {
        return new EmployeeId(value);
    }

    public static EmployeeId CreateUnique()
    {
        return new EmployeeId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEquallyComponents()
    {
        yield return Value;
    }
}