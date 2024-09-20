namespace FRESHY.Main.Contract.Requests.EmployeeRequests;

public record CreateEmployeeProfileRequest
(
    Guid AccountId,
    string Email,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string Ssn,
    string Dob,
    string LivingAddress,
    string Hometown,
    string CvLink,
    Guid JobPositionId,
    Guid? ManagerId
);