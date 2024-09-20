using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;

public sealed class Supplier : AggregateRoot<SupplierId>
{
    private readonly List<Product> _products = new();

    private Supplier(
        SupplierId id,
        string name,
        string? description,
        bool isValid,
        DateTime joinedDate,
        string featureImage) : base(id)
    {
        Name = name;
        Description = description;
        IsValid = isValid;
        JoinedDate = joinedDate;
        FeatureImage = featureImage;
    }

    #region Properties

    public string Name { get; private set; }
    public string FeatureImage { get; private set; }
    public string? Description { get; private set; }
    public bool IsValid { get; private set; }
    public DateTime JoinedDate { get; private set; }
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    #endregion Properties

    #region Function

    public static Supplier Create(
        string name,
        string featureImage,
        string? description,
        bool isValid)
    {
        return new Supplier(
            SupplierId.CreateUnique(),
            name,
            description,
            isValid,
            DateTime.Today,
            featureImage);
    }

    #endregion Function

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Supplier()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}