namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;

public record ProductUnitResult
(
    Guid UnitId,
    string UnitType,
    double UnitValue,
    int Quantity,
    double ImportPrice,
    double SellPrice,
    string UnitFeatureImage
);