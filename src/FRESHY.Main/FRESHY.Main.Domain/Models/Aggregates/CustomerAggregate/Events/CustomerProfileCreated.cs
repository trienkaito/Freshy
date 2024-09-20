using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Events;

public class CustomerProfileCreated : DomainEvent
{
    public CustomerProfileCreated(
        Guid aggregateId,
        Guid accountId) : base(aggregateId)
    {
        AccountId = accountId;
        Reason = "New Customer Profile Created";
    }

    #region Properties

    public Guid AccountId { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}