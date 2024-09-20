using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllCustomerOrders.Results;

public record AllCustomerOrdersResult
(
    Guid OrderId,
    Guid CustomerId,
    string CustomerName,
    string? OrderAddress,
    DateTime CreatedDate,
    OrderStatusResult OrderStatus,
    PaymentTypeResult PaymentType,
    OrderShippingResult? Shipping,
    OrderVoucherResult? Voucher,
    double PaidAmount
);