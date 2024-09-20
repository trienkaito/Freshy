namespace FRESHY.Main.Contract.Responses.ProductResponses.Shared;

public record ProductSupplierResponse
(
    Guid SupplierId,
    string Name,
    bool IsValid
);