namespace FRESHY.Main.Contract.Requests.OrderRequests.Shared;

public record CreateOrderItemRequest
(
    Guid ProductId,
    Guid UnitId,
    int BoughtQuantity
);