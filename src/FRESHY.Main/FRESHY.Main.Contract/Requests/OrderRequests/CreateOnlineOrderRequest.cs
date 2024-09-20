using FRESHY.Main.Contract.Requests.OrderRequests.Shared;
using FRESHY.Main.Contract.Requests.Shared;

namespace FRESHY.Main.Contract.Requests.OrderRequests;

public record CreateOnlineOrderRequest
(
    Guid CustomerId,
    string OrderAddress,
    List<CreateOrderItemRequest> OrderItems,
    string PaymentType,
    Guid ShippingId,
    VoucherCodeRequest VoucherCode
);