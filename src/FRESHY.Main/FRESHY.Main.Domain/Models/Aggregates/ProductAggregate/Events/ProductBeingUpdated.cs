using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;

public class ProductBeingUpdated : DomainEvent
{
    public ProductBeingUpdated(
        Guid aggregateId,
        Guid updatedBy) : base(aggregateId)
    {
        UpdatedBy = updatedBy;
        Reason = "Product Being Updated";
    }

    #region Properties

    public Guid UpdatedBy { get; private set; }
    public string Reason { get; private set; }

    #endregion Properties
}