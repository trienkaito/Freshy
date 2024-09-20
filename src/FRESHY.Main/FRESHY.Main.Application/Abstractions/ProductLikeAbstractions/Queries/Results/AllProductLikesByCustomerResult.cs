using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Application.Abstractions.ProductLikeAbstractions.Queries.Resutls;

public record AllProductLikesByCustomerResult
(
    Guid Id,
    string Name,
    string FeatureImage,
    Guid TypeId,
    string TypeName,
    double LowestPrice,
    Guid ProductLikeId
);