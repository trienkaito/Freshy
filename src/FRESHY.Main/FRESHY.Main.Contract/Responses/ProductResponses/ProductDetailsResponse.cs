using FRESHY.Main.Contract.Responses.ProductResponses.Shared;

namespace FRESHY.Main.Contract.Responses.ProductResponses;

public record ProductDetailsResponse
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    ProductTypeResponse Type,
    ProductSupplierResponse Supplier,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    DateTime Dom,
    DateTime ExpiryDate,
    bool IsShowToCustomer,
    List<ProductUnitResponse>? Units
);