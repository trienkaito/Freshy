namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record AllCustomerOrdersResponse
(
    Guid OrderId,
    Guid CustomerId,
    string CustomerName,
    string? OrderAddress,   
    DateTime CreatedDate,
    OrderStatusResponse OrderStatus,
    PaymentTypeResponse PaymentType,
    OrderShippingResponse? Shipping,
    OrderVoucherResponse? Voucher,
    double PaidAmount
);