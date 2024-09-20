using FRESHY.Common.Domain.Common.Models;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate;

namespace FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;

public sealed class Employee : AggregateRoot<EmployeeId>
{
    private readonly List<Review> _reviews = new();

    private Employee(
        EmployeeId id,
        Guid accountId,
        string email,
        string fullname,
        string avatar,
        string phoneNumber,
        string sSN,
        DateTime dOB,
        string livingAddress,
        string hometown,
        string cvLink,
        DateTime createdDate,
        DateTime? updatedDate,
        JobPositionId jobPositionId,
        EmployeeId? managerId) : base(id)
    {
        AccountId = accountId;
        Fullname = fullname;
        PhoneNumber = phoneNumber;
        SSN = sSN;
        DOB = dOB;
        LivingAddress = livingAddress;
        Hometown = hometown;
        CvLink = cvLink;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        JobPositionId = jobPositionId;
        ManagerId = managerId;
        Avatar = avatar;
        Email = email;
    }

    #region Properties

    public Guid AccountId { get; private set; }
    public string Email { get; private set; }
    public string Fullname { get; private set; }
    public string Avatar { get; private set; }
    public string PhoneNumber { get; private set; }
    public string SSN { get; private set; }
    public DateTime DOB { get; private set; }
    public string LivingAddress { get; private set; }
    public string Hometown { get; private set; }
    public string CvLink { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public EmployeeId? ManagerId { get; private set; }
    public JobPositionId JobPositionId { get; set; }
    public JobPosition JobPosition { get; private set; } = null!;
    public IReadOnlyList<Review> Reviews => _reviews.AsReadOnly();

    #endregion Properties

    #region Functions

    public static Employee Create
    (
        string email,
        string fullname,
        string avatar,
        string phoneNumber,
        string sSN,
        DateTime dOB,
        string livingAddress,
        string hometown,
        string cvLink,
        JobPositionId jobPositionId,
        EmployeeId? managerId,
        Guid accountId
    )
    {
        return new Employee(
            EmployeeId.CreateUnique(),
            accountId,
            email,
            fullname,
            avatar,
            phoneNumber,
            sSN,
            dOB,
            livingAddress,
            hometown,
            cvLink,
            DateTime.UtcNow,
            null,
            jobPositionId,
            managerId);
    }

    public void UpdateEmployeeInfos(
        string fullname,
        string avatar,
        string phoneNumber,
        string sSN,
        DateTime dOB,
        string livingAddress,
        string hometown,
        string cvLink,
        JobPositionId jobPositionId,
        EmployeeId? managerId
    )
    {
        Fullname = fullname;
        Avatar = avatar;
        PhoneNumber = phoneNumber;
        SSN = sSN;
        DOB = dOB;
        LivingAddress = livingAddress;
        Hometown = hometown;
        CvLink = cvLink;
        JobPositionId = jobPositionId;
        ManagerId = managerId;
    }

    #endregion Functions

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Employee()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}