using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductsOfASupplier.Results;

public record ProductsOfASupplierResult
(
    Guid Id,
    string Name,
    string FeatureImage,
    ProductTypeResult Type
);