namespace FRESHY.Main.Contract.Requests.CustomerRequests;

public record CreateCustomerProfileRequest
(
    Guid AccountId,
    string? Email,
    string? Phone,
    string Name
);