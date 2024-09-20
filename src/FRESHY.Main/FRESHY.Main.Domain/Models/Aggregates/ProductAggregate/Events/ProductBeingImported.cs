using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;

public class ProductBeingImported : DomainEvent
{
    public ProductBeingImported(
        Guid aggregateId,
        Guid importedBy,
        Dictionary<Guid, int> importedQuantityPerUnit) : base(aggregateId)
    {
        ImportedBy = importedBy;
        Reason = "Product Imported";
        ImportedQuantityPerUnit = importedQuantityPerUnit;
    }

    #region Properties

    public Guid ImportedBy { get; private set; }
    public string Reason { get; private set; }
    public Dictionary<Guid, int> ImportedQuantityPerUnit { get; private set; }

    #endregion Properties
}