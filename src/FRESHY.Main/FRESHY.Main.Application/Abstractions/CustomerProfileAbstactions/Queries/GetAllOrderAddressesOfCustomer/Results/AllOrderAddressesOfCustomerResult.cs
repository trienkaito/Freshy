namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Queries.GetAllOrderAddressesOfCustomer.Results;

public record AllOrderAddressesOfCustomerResult
(
    Guid Id,
    string Name,
    string PhoneNumber,
    string Country,
    string City,
    string District,
    string Ward,
    string Details,
    bool IsDefaultAddress
);