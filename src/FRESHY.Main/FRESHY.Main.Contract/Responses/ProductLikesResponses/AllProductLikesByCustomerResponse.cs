namespace FRESHY.Main.Contract.Responses.ProductLikesResponses;

public record AllProductLikesByCustomerResponse
(
    Guid Id,
    string Name,
    string FeatureImage,
    Guid TypeId,
    string TypeName,
    double LowestPrice,
    Guid ProductLikeId
);