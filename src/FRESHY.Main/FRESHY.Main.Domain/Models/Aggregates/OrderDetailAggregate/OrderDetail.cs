using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Helpers;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.OrderItemAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate;

public sealed class OrderDetail : AggregateRoot<OrderDetailId>
{
    private readonly List<OrderItem> _orderItems = new();

    private OrderDetail(
        OrderDetailId id,
        CustomerId customerId,
        string? orderAddress,
        DateTime createdDate,
        OrderStatus orderStatus,
        double productsAmount,
        PaymentType paymentType,
        ShippingId? shippingId,
        VoucherId? voucherId,
        double paidAmount) : base(id)
    {
        CustomerId = customerId;
        OrderAddress = orderAddress;
        CreatedDate = createdDate;
        OrderStatus = orderStatus;
        ProductsAmount = productsAmount;
        PaymentType = paymentType;
        ShippingId = shippingId;
        VoucherId = voucherId;
        PaidAmount = paidAmount;
    }

    #region Properties

    public CustomerId CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public string? OrderAddress { get; private set; }
    public DateTime CreatedDate { get; private set; }

    [EnumDataType(typeof(OrderStatus))]
    public OrderStatus OrderStatus { get; private set; }

    [EnumDataType(typeof(PaymentType))]
    public PaymentType PaymentType { get; private set; }

    public ShippingId? ShippingId { get; private set; }
    public Shipping? Shipping { get; private set; }
    public VoucherId? VoucherId { get; private set; }
    public Voucher? Voucher { get; private set; }
    public double ProductsAmount { get; private set; }
    public double PaidAmount { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    #endregion Properties

    #region Functions

    public static OrderDetail CreateOfflineOrder(
        OrderDetailId id,
        CustomerId customerId,
        double productsAmount,
        PaymentType paymentType,
        VoucherId? voucherId,
        double paidAmount
    )
    {
        return new OrderDetail(
            id,
            customerId,
            null,
            DateTime.UtcNow,
            OrderStatus.CREATED,
            productsAmount,
            paymentType,
            null,
            voucherId,
            paidAmount
        );
    }

    public static OrderDetail CreateOnlineOrder(
        OrderDetailId id,
        CustomerId customerId,
        string? orderAddress,
        double productsAmount,
        PaymentType paymentType,
        ShippingId? shippingId,
        VoucherId? voucherId,
        double paidAmount
    )
    {
        return new OrderDetail(
            id,
            customerId,
            orderAddress,
            DateTime.UtcNow,
            OrderStatus.CREATED,
            productsAmount,
            paymentType,
            shippingId,
            voucherId,
            paidAmount
        );
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private OrderDetail()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}