using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Events;

public class OfflineOrderCreated : DomainEvent
{
    public OfflineOrderCreated(
        Guid aggregateId,
        Guid customerId,
        Guid orderDetail,
        Guid employeeId) : base(aggregateId)
    {
        CustomerId = customerId;
        OrderDetail = orderDetail;
        Reason = "New Offline Order Created";
        EmployeeId = employeeId;
    }

    #region Properties

    public Guid CustomerId { get; private set; }
    public Guid OrderDetail { get; private set; }
    public string Reason { get; private set; }
    public Guid EmployeeId { get; private set; }

    #endregion Properties
}