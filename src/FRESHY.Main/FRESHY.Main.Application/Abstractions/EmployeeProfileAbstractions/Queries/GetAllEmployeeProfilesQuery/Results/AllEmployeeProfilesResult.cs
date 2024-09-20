namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeeProfilesQuery.Results;

public record AllEmployeeProfilesResult
(
    Guid AccountId,
    Guid EmployeeId,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string Ssn,
    string Hometown
);