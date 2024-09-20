namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetCustomerProfileDetails.Results;

public record CustomerProfileDetailsResult
(
    string Name,
    string Avatar,
    Guid AccountId,
    DateTime CreatedDate,
    int OrderAddressesCount,
    int OrderDetailsCount,
    int ReviewsCount
);