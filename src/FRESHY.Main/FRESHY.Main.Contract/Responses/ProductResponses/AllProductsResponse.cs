using FRESHY.Main.Contract.Responses.ProductResponses.Shared;

namespace FRESHY.Main.Contract.Responses.ProductResponses;

public record AllProductsResponse
(
    Guid ProductId,
    string Name,
    string FeatureImage,
    string Description,
    ProductTypeResponse Type,
    ProductSupplierResponse Supplier,
    DateTime Dom,
    DateTime ExpiryDate,
   bool IsShowToCustomer,
    List<ProductUnitResponse>? Units
);