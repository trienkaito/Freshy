namespace FRESHY.Main.Contract.Requests.ProductLikesRequests;

public record AllProductLikesByCustomerRequest
(
    Guid ProductId,
    Guid CustomerId
);