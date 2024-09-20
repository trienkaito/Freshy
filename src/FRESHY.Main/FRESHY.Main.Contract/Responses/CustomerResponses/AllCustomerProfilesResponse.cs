namespace FRESHY.Main.Contract.Responses.CustomerResponses;

public record AllCustomerProfilesResponse
(
    Guid AccountId,
    Guid CustomerId,
    string Email,
    string Phone,
    string Name,
    string? Avatar,
    DateTime CreatedDate,
    double TotalSpent
);