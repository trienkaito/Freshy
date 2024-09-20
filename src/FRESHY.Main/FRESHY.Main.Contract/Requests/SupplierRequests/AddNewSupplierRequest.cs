namespace FRESHY.Main.Contract.Requests.SupplierRequests;

public record AddNewSupplierRequest
(
    string Name,
    string FeatureImage,
    string? Description,
    bool IsValid,
    Guid EmployeeId
);