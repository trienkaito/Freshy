namespace FRESHY.Main.Contract.Requests.ProductLikesRequests;

public record DeleteProductLikeRequest
(
    Guid CustomerId,
    Guid ProductId
);