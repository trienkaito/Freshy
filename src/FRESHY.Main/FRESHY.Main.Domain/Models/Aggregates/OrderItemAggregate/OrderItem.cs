using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;

public sealed class OrderItem : AggregateRoot<OrderItemId>
{
    private OrderItem(
        OrderItemId id,
        OrderDetailId orderDetailId,
        ProductId productId,
        ProductUnitId productUnitId,
        int boughtQuantity,
        double totalPrice) : base(id)
    {
        ProductId = productId;
        BoughtQuantity = boughtQuantity;
        TotalPrice = totalPrice;
        ProductUnitId = productUnitId;
        OrderDetailId = orderDetailId;
    }

    #region Properties

    public Product Product { get; private set; } = null!;
    public ProductId ProductId { get; private set; }
    public ProductUnit ProductUnit { get; private set; } = null!;
    public ProductUnitId ProductUnitId { get; private set; }
    public int BoughtQuantity { get; private set; }
    public double TotalPrice { get; private set; }
    public OrderDetail OrderDetail { get; private set; } = null!;
    public OrderDetailId OrderDetailId { get; private set; }

    #endregion Properties

    #region Functions

    public static OrderItem Create(
        OrderDetailId orderDetailId,
        ProductId productId,
        ProductUnitId productUnitId,
        int boughtQuantity,
        double totalPrice
    )
    {
        return new OrderItem(
            OrderItemId.CreateUnique(),
            orderDetailId,
            productId,
            productUnitId,
            boughtQuantity,
            totalPrice
        );
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private OrderItem()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}