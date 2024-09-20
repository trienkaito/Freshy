using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;

public record AllProductsResult
(
    Guid ProductId,
    string Name,
    string FeatureImage,
    string Description,
    ProductTypeResult Type,
    ProductSupplierResult Supplier,
    DateTime Dom,
    DateTime ExpiryDate,
    bool IsShowToCustomer,
    List<ProductUnitResult>? Units
);