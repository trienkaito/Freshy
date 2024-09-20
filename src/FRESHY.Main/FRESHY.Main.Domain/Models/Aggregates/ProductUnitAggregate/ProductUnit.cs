using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;

public sealed class ProductUnit : AggregateRoot<ProductUnitId>
{
    private readonly List<CartItem> _cartItems = new();
    private readonly List<OrderItem> _orderItems = new();

    public ProductUnit(
        ProductUnitId id,
        ProductId productId,
        string unitType,
        double unitValue,
        int quantity,
        double importPrice,
        double sellPrice,
        string unitFeatureImage) : base(id)
    {
        UnitType = unitType;
        UnitValue = unitValue;
        Quantity = quantity;
        ImportPrice = importPrice;
        SellPrice = sellPrice;
        ProductId = productId;
        UnitFeatureImage = unitFeatureImage;
    }

    #region Properties

    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; }
    public string UnitType { get; private set; }
    public double UnitValue { get; private set; }
    public int Quantity { get; private set; }
    public double ImportPrice { get; private set; }
    public double SellPrice { get; private set; }
    public string UnitFeatureImage { get; private set; }
    public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    #endregion Properties

    #region Functions

    public static ProductUnit Create(
        ProductId productId,
        string unitType,
        double unitValue,
        int quantity,
        double importPrice,
        double sellPrice,
        string unitFeatureImage)
    {
        return new ProductUnit(
            ProductUnitId.CreateUnique(),
            productId,
            unitType.ToUpper(),
            unitValue,
            quantity,
            importPrice,
            sellPrice,
            unitFeatureImage);
    }

    public void Update(
        string unitType,
        double unitValue,
        int quantity,
        double importPrice,
        double sellPrice,
        string unitFeatureImage
    )
    {
        UnitType = unitType.ToUpper();
        UnitValue = unitValue;
        Quantity = quantity;
        ImportPrice = importPrice;
        SellPrice = sellPrice;
        UnitFeatureImage = unitFeatureImage;
    }

    public void SubstractUnitStock(int value)
    {
        Quantity -= value;
    }

    public void AddUnitStock(int value)
    {
        Quantity += value;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ProductUnit()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}