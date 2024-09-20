namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Queries.GetOrderDetails.Results;

public record OrderCustomerResult
(
    Guid CustomerId,
    string Name,
    string Avatar
);