namespace FRESHY.Main.Contract.Responses.ProductResponses.Shared;

public record ProductUnitResponse
(
    Guid UnitId,
    string UnitType,
    double UnitValue,
    int Quantity,
    double ImportPrice,
    double SellPrice,
    string UnitFeatureImage
);