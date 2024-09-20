namespace FRESHY.Main.Contract.Requests.ProductRequests;
public record DeActiveProductRequest
(
    Guid ProductId,
    Guid EmmployeeId
);