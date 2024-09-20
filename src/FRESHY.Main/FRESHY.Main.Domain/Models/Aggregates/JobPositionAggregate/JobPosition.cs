using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;

public sealed class JobPosition : AggregateRoot<JobPositionId>
{
    private readonly List<Employee> _employees = new();

    private JobPosition(
        JobPositionId id,
        string name,
        string? description,
        double salary) : base(id)
    {
        Name = name;
        Description = description;
        Salary = salary;
    }

    #region Properties

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public double Salary { get; private set; }
    public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

    #endregion Properties

    #region Function

    public static JobPosition Create
    (
        string name,
        string? description,
        double salary
    )
    {
        return new JobPosition(
            JobPositionId.CreateUnique(),
            name.ToUpper(),
            description,
            salary);
    }

    public void UpdateJobPositon(
        string name,
        string? description,
        double salary
    )
    {
        Name = name;
        Description = description;
        Salary = salary;
    }

    #endregion Function

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private JobPosition()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}