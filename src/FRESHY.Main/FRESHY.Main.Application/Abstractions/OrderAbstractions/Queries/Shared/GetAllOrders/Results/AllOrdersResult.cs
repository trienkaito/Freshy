using FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetAllOrders.Results;

public record AllOrdersResult
(
    Guid CustomerId,
    Guid OrderId,
    string? OrderAddress,
    DateTime CreatedDate,
    OrderStatusResult OrderStatus,
    PaymentTypeResult PaymentType,
    double PaidAmount
);