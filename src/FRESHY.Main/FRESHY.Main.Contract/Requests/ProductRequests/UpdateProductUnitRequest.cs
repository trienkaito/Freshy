namespace FRESHY.Main.Contract.Requests.ProductRequests;

public record UpdateProductUnitRequest
(
    Guid ProductId,
    double ImportPrice,
    int Quantity,
    double SellPrice,
    string UnitFeatureImage,
    string UnitType,
    double UnitValue,
    Guid ProductUnitId

);