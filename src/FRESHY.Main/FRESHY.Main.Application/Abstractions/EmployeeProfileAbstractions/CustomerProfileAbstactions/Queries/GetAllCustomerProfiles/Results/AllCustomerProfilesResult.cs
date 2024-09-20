namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.CustomerProfileAbstactions.Queries.GetAllCustomerProfiles.Results;

public record AllCustomerProfilesResult
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