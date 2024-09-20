namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetEmployeeProfileDetailsQuery.Results;

public record EmployeeProfileDetailsResult
(
    Guid AccountId,
    string Email,
    string Fullname,
    string Avatar,
    string PhoneNumber,
    string SSN,
    DateTime DOB,
    string LivingAddress,
    string Hometown,
    string CvLink,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    string JobPosition,
    string ManagerName
);