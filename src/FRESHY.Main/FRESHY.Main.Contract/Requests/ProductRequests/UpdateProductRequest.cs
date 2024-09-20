namespace FRESHY.Main.Contract.Requests.ProductRequests;
public record UpdateProductRequest
(
    Guid Id,
    string Name,
    string FeatureImage,
    string Description,
    Guid TypeId,
    Guid SupplierId,
    string Dom,
    string ExpiryDate,
    bool IsShowToCustomer,
    Guid EmployeeId
);