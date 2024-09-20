namespace FRESHY.Main.Contract.Responses.CustomerResponses;

public record AllOrderAddressesOfCustomerResponse
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