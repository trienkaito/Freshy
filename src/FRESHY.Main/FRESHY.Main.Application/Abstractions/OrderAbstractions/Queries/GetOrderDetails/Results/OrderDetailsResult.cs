using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;

public record OrderDetailsResult
(
    Guid OrderId,
    OrderCustomerResult Customer,
    string? OrderAddress,
    DateTime CreatedDate,
    OrderStatusResult OrderStatus,
    double ProductsAmount,
    PaymentTypeResult PaymentType,
    OrderShippingResult? Shipping,
    OrderVoucherResult? Voucher,
    List<OrderItemResult> OrderItems,
    double PaidAmount
);