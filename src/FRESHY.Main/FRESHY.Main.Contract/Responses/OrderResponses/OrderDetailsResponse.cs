namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record OrderDetailsResponse
(
    Guid OrderId,
    OrderCustomerResponse Customer,
    string? OrderAddress,
    DateTime CreatedDate,
    OrderStatusResponse OrderStatus,
    double ProductsAmount,
    PaymentTypeResponse PaymentType,
    OrderShippingResponse? Shipping,
    OrderVoucherResponse? Voucher,
    List<OrderItemResponse> OrderItems,
    double PaidAmount
);