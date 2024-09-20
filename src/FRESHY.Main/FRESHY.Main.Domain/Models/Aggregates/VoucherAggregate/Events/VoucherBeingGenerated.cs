using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.Events;

public class VoucherBeingGenerated : DomainEvent
{
    public VoucherBeingGenerated(
        Guid aggregateId,
        Guid createdBy) : base(aggregateId)
    {
        CreatedBy = createdBy;
        Reason = "New Voucher Being Generated";
    }

    #region Properties

    public Guid CreatedBy { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}