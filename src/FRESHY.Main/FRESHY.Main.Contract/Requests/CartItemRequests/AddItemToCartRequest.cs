namespace FRESHY.Main.Contract.Requests.CartItemRequests;

public record AddItemToCartRequest
(
    Guid CustomerId,
    Guid ProductId,
    Guid ProductUnitId,
    int BoughtQuantity
);