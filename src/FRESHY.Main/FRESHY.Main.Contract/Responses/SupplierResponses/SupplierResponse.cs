namespace FRESHY.Main.Contract.Responses.SupplierResponses;

public record SupplierResponse
(
    Guid Id,
    string Name,
    string FeatureImage,
    string? Description,
    bool IsValid,
    string ProductCount
);