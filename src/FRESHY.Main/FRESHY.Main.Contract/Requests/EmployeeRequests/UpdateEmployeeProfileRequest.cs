namespace FRESHY.Main.Contract.Requests.EmployeeRequests;

public record UpdateEmployeeProfileRequest
(
    Guid EmployeeId,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string SSN,
    DateTime DOB,
    string LivingAddress,
    string Hometown,
    string CvLink,
    Guid? ManagerId,
    Guid JobPositionId
);