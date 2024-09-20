using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.CartItemAggregate;

public sealed class CartItem : AggregateRoot<CartItemId>
{
    private CartItem(
        CartItemId id,
        CartId cartId,
        ProductId productId,
        ProductUnitId productUnitId,
        int boughtQuantity,
        double totalPrice,
        DateTime addedDate) : base(id)
    {
        ProductId = productId;
        BoughtQuantity = boughtQuantity;
        TotalPrice = totalPrice;
        AddedDate = addedDate;
        ProductUnitId = productUnitId;
        CartId = cartId;
    }

    #region Properties

    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; }
    public ProductUnit ProductUnit { get; private set; } = null!;
    public ProductUnitId ProductUnitId { get; private set; }
    public int BoughtQuantity { get; private set; }
    public double TotalPrice { get; private set; }
    public DateTime AddedDate { get; private set; }
    public Cart Cart { get; private set; } = null!;
    public CartId CartId { get; private set; }

    #endregion Properties

    #region Functions

    public static CartItem AddToCart(
        CartId cartId,
        ProductId productId,
        ProductUnitId productUnitId,
        int boughtQuantity,
        double totalPrice
    )
    {
        return new CartItem(
            CartItemId.CreateUnique(),
            cartId,
            productId,
            productUnitId,
            boughtQuantity,
            totalPrice,
            DateTime.UtcNow);
    }

    public void UpdateQuantity(int addedQuantity, double addedPriceAmount)
    {
        BoughtQuantity += addedQuantity;
        TotalPrice += addedPriceAmount;
    }

    public void UpdatePrice(double addedPriceAmount)
    {
        TotalPrice += addedPriceAmount;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private CartItem()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}