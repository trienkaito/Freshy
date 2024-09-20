using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;

public sealed class Product : AggregateRoot<ProductId>
{
    private readonly List<ProductUnit> _units = new();
    private readonly List<CartItem> _cartItems = new();
    private readonly List<OrderItem> _orderItems = new();
    private readonly List<ProductLike> _likes = new();
    private readonly List<Review> _reviews = new();

    private Product(
        ProductId id,
        string name,
        string featureImage,
        string description,
        ProductTypeId typeId,
        SupplierId supplierId,
        DateTime createdDate,
        DateTime? updatedDate,
        DateTime dOM,
        DateTime expiryDate,
        bool isShowToCustomer) : base(id)
    {
        Name = name;
        FeatureImage = featureImage;
        Description = description;
        TypeId = typeId;
        SupplierId = supplierId;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        DOM = dOM;
        ExpiryDate = expiryDate;
        IsShowToCustomer = isShowToCustomer;
    }

    #region Properties

    public string Name { get; private set; }
    public string FeatureImage { get; private set; }
    public string Description { get; private set; }
    public ProductTypeId TypeId { get; private set; }
    public ProductType Type { get; private set; } = null!;
    public SupplierId SupplierId { get; private set; }
    public Supplier Supplier { get; private set; } = null!;
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public DateTime DOM { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsShowToCustomer { get; private set; }
    public IReadOnlyList<ProductUnit> Units => _units.AsReadOnly();
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public IReadOnlyList<ProductLike> Likes => _likes.AsReadOnly();
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Product Create(
        string name,
        string featureImage,
        string description,
        ProductTypeId typeId,
        SupplierId supplierId,
        DateTime dOM,
        DateTime expiryDate,
        bool isShowToCustomer)
    {
        return new Product
        (
            ProductId.CreateUnique(),
            name,
            featureImage,
            description,
            typeId,
            supplierId,
            DateTime.UtcNow,
            null,
            dOM,
            expiryDate,
            isShowToCustomer
        );
    }

    public void UpdateProductInfos(
        string name,
        string featureImage,
        string description,
        ProductTypeId typeId,
        SupplierId supplierId,
        DateTime dOM,
        DateTime expiryDate,
        bool isShowToCustomer
    )
    {
        Name = name;
        Description = description;
        TypeId = typeId;
        SupplierId = supplierId;
        DOM = dOM;
        ExpiryDate = expiryDate;
        IsShowToCustomer = isShowToCustomer;
        FeatureImage = featureImage;
        UpdatedDate = DateTime.UtcNow;
    }

    public void DeActiveProduct()
    {
        IsShowToCustomer = false;
    }

    public void ActiveProduct()
    {
        IsShowToCustomer = true;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Product()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}