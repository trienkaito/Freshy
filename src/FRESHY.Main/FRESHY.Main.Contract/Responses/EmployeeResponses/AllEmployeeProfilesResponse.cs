namespace FRESHY.Main.Contract.Responses.EmployeeResponses;

public record AllEmployeeProfilesResponse
(
    Guid AccountId,
    Guid EmployeeId,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string Ssn,
    string Hometown
);