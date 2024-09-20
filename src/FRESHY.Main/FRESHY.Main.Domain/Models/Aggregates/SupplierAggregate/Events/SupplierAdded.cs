using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.Events;

public class SupplierAdded : DomainEvent
{
    public SupplierAdded(
        Guid aggregateId,
        Guid supplier,
        Guid employeeId) : base(aggregateId)
    {
        SupplierId = supplier;
        Reason = "New Supplier Being Added";
        EmployeeId = employeeId;
    }

    #region Properties

    public Guid SupplierId { get; private set; }
    public string Reason { get; private set; }
    public Guid EmployeeId { get; private set; }

    #endregion Properties
}