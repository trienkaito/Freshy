namespace FRESHY.Main.Contract.Requests.CustomerRequests;

public record UpdateCustomerProfileRequest
(
    Guid CustomerId,
    string Name,
    string Avatar
);