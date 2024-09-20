using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.UpdateEmployeeProfile;

public record UpdateEmployeeProfileCommand
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
) : ICommand<CommandResult>;

public class UpdateEmployeeProfileCommandHandler : ICommandHandler<UpdateEmployeeProfileCommand, CommandResult>
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public UpdateEmployeeProfileCommandHandler(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }

    public async Task<CommandResult> Handle(UpdateEmployeeProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _employeeProfileRepository.UnitOfWork.BeginTransaction();

            var employee = await _employeeProfileRepository.GetByIdAsync(EmployeeId.Create(request.EmployeeId));

            if (employee is not null)
            {
                employee.UpdateEmployeeInfos(
                    request.Fullname,
                    request.Avatar,
                    request.PhoneNumber,
                    request.SSN,
                    Convert.ToDateTime(request.DOB),
                    request.LivingAddress,
                    request.Hometown,
                    request.CvLink,
                    JobPositionId.Create(request.JobPositionId),
                    request.ManagerId is null ? null : EmployeeId.Create((Guid)request.ManagerId)
                );

                await _employeeProfileRepository.UnitOfWork.Commit(cancellationToken);
                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, Error.EMPLOYEE_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}