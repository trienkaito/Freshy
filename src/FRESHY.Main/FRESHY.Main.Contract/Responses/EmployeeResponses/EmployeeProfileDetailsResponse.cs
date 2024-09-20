namespace FRESHY.Main.Contract.Responses.EmployeeResponses;

public record EmployeeProfileDetailsResponse
(
    Guid AccountId,
    Guid EmployeeId,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string Ssn,
    string HomeTown,
    EmployeeJobPositionResponse JobPosition
);