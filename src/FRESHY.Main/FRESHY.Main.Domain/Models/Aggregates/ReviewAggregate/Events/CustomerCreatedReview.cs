using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;

public class CustomerCreatedReview : DomainEvent
{
    public CustomerCreatedReview(
        Guid aggregateId,
        Guid reviewedBy) : base(aggregateId)
    {
        ReviewedBy = reviewedBy;
        Reason = "Product Had A New Review";
    }

    #region Properties

    public Guid ReviewedBy { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}