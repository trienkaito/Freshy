namespace FRESHY.Main.Contract.Requests.JobPositionRequests;

public record UpdateJobPositionRequest
(
    Guid Id,
    string Name,
    string? Description,
    double Salary
);