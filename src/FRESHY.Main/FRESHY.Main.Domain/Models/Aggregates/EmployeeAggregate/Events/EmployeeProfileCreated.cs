using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.Events;

public class EmployeeProfileCreated : DomainEvent
{
    public EmployeeProfileCreated(
        Guid aggregateId,
        Guid accountId,
        Guid employeeId) : base(aggregateId)
    {
        AccountId = accountId;
        EmployeeId = employeeId;
        Reason = "New Employee Profile Created";
    }

    #region Properties

    public Guid AccountId { get; private set; }
    public Guid EmployeeId { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}