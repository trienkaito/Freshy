namespace FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Queries.GetAllJobPositions.Results;

public record AllJobPositionsResult
(
    Guid Id,
    string Name,
    string? Description,
    double Salary
);