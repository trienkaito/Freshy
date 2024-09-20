using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Events;

public class OnlineOrderCreated : DomainEvent
{
    public OnlineOrderCreated(
        Guid aggregateId,
        Guid customerId,
        Guid orderDetail) : base(aggregateId)
    {
        CustomerId = customerId;
        OrderDetail = orderDetail;
        Reason = "New Online Order Created";
    }

    #region Properties

    public Guid CustomerId { get; private set; }
    public Guid OrderDetail { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}