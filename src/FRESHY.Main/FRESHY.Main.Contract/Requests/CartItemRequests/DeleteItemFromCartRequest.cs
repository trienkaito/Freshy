namespace FRESHY.Main.Contract.Requests.CartItemRequests;

public record DeleteItemFromCartRequest
(
    Guid CustomerId,
    Guid CartItemId
);