namespace FRESHY.Main.Contract.Requests.JobPositionRequests;

public record CreateJobPositionRequest
(
    string Name,
    string? Description,
    double Salary
);