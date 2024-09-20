namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;

public record ProductSupplierResult
(
    Guid SupplierId,
    string Name,
    bool IsValid
);