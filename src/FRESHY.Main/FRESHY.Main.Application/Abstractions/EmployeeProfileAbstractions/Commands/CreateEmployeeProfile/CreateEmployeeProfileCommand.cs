using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.CreateEmployeeProfile;

public record CreateEmployeeProfileCommand
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
) : ICommand<CommandResult>;

public class CreateCustomerProfileCommandHandler : ICommandHandler<CreateEmployeeProfileCommand, CommandResult>
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public CreateCustomerProfileCommandHandler(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }

    public async Task<CommandResult> Handle(CreateEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _employeeProfileRepository.UnitOfWork.BeginTransaction();

            var employeeProfile = Employee.Create(
                request.Email,
                request.Fullname,
                request.Avatar,
                request.PhoneNumber,
                request.Ssn,
                Convert.ToDateTime(request.Dob),
                request.LivingAddress,
                request.Hometown,
                request.CvLink,
                JobPositionId.Create(request.JobPositionId),
                request.ManagerId is not null ? EmployeeId.Create((Guid)request.ManagerId) : null,
                request.AccountId
            );

            await _employeeProfileRepository.InsertAsync(employeeProfile);
            await _employeeProfileRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}