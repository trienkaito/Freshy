namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.Shared.Results;

public record SupplierResult
(
    Guid Id,
    string Name,
    string FeatureImage,
    string? Description,
    bool IsValid,
    string ProductCount
);