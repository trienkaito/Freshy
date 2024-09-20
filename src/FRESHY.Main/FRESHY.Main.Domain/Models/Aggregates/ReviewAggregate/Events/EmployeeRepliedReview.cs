using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;

public class EmployeeRepliedReview : DomainEvent
{
    public EmployeeRepliedReview(
        Guid aggregateId,
        Guid repliedBy,
        Guid reviewBeingRepied) : base(aggregateId)
    {
        RepliedBy = repliedBy;
        Reason = "A Review Being Replied";
        ReviewBeingRepied = reviewBeingRepied;
    }

    #region Properties

    public Guid RepliedBy { get; private set; }
    public Guid ReviewBeingRepied { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}