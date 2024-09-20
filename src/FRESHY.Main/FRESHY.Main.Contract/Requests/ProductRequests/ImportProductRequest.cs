namespace FRESHY.Main.Contract.Requests.ProductRequests;

public record ImportProductRequest
(
    string Name,
    string FeatureImage,
    string Description,
    Guid TypeId,
    Guid SupplierId,
    string Dom,
    string ExpiryDate,
    bool IsShowToCustomer,
    Guid EmployeeId,
    List<CreateProductUnitRequest>? Units
);

public record CreateProductUnitRequest
(
    double ImportPrice,
    int Quantity,
    double SellPrice,
    string UnitFeatureImage,
    string UnitType,
    double UnitValue

);