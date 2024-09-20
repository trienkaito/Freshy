namespace FRESHY.Main.Contract.Responses.JobPositionResponses;

public record AllJobPositionsResponse
(
    Guid Id,
    string Name,
    string? Description,
    double Salary
);