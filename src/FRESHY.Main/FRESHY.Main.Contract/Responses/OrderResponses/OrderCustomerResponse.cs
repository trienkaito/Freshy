namespace FRESHY.Main.Contract.Responses.OrderResponses;

public record OrderCustomerResponse
(
    Guid CustomerId,
    string Name,
    string Avatar
);