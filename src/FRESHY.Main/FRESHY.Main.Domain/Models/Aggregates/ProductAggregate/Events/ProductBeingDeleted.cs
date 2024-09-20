using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;

public class ProductBeingDeActivated : DomainEvent
{
    public ProductBeingDeActivated(
        Guid aggregateId,
        Guid deletedBy) : base(aggregateId)
    {
        DeletedBy = deletedBy;
        Reason = "Product Being Deactive";
    }

    #region Properties

    public Guid DeletedBy { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}