using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;

public sealed class ProductType : AggregateRoot<ProductTypeId>
{
    private readonly List<Product> _products = new();

    private ProductType(
        ProductTypeId id,
        string name) : base(id)
    {
        Name = name;
    }

    #region Properties

    public string Name { get; private set; }
    public IReadOnlyList<Product> Products => _products.AsReadOnly();

    #endregion Properties

    #region Functions

    public static ProductType Create(string name)
    {
        return new ProductType(
            ProductTypeId.CreateUnique(),
            name.ToUpper());
    }

    public void UpdateProductType(string name)
    {
        Name = name.ToUpper();
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private ProductType()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}