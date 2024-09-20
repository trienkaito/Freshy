using FRESHY.Main.Contract.Responses.ProductResponses.Shared;

namespace FRESHY.Main.Contract.Responses.ProductResponses;

public record ProductsOfASupplierResponse
(
    Guid Id,
    string Name,
    string FeatureImage,
    ProductTypeResponse Type
);