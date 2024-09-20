using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductDetails.Results;

public record ProductDetailsResult
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    ProductTypeResult Type,
    ProductSupplierResult Supplier,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    DateTime Dom,
    DateTime ExpiryDate,
    bool IsShowToCustomer,
    List<ProductUnitResult>? Units
);