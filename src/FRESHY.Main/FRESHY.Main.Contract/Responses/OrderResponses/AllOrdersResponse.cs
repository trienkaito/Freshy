namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record AllOrdersResponse
(
    Guid CustomerId,
    Guid OrderId,
    string? OrderAddress,
    DateTime CreatedDate,
    OrderStatusResponse OrderStatus,
    PaymentTypeResponse PaymentType,
    double PaidAmount
);