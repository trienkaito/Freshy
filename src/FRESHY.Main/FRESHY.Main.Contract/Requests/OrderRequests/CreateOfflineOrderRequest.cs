using FRESHY.Main.Contract.Requests.OrderRequests.Shared;
using FRESHY.Main.Contract.Requests.Shared;

namespace FRESHY.Main.Contract.Requests.OrderRequests;

public record CreateOfflineOrderRequest
(
    Guid? CustomerId,
    List<CreateOrderItemRequest> OrderItems,
    string PaymentType,
    VoucherCodeRequest VoucherCode,
    Guid EmployeeId
);