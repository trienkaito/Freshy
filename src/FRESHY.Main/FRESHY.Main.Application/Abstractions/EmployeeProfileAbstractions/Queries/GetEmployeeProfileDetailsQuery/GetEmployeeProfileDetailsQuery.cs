using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetEmployeeProfileDetailsQuery.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Errors;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetEmployeeProfileDetailsQuery;

public record GetEmployeeProfileDetailsQuery
(
    Guid EmployeeId
) : IQuery<QueryResult<EmployeeProfileDetailsResult>>;

public class GetEmployeeProfileDetailsQueryHandler : IQueryHandler<GetEmployeeProfileDetailsQuery, QueryResult<EmployeeProfileDetailsResult>>
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;
    private readonly IJobPositionRepository _jobPositionRepository;

    public GetEmployeeProfileDetailsQueryHandler(
        IEmployeeProfileRepository employeeProfileRepository,
        IJobPositionRepository jobPositionRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
        _jobPositionRepository = jobPositionRepository;
    }

    public async Task<QueryResult<EmployeeProfileDetailsResult>> Handle(GetEmployeeProfileDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _employeeProfileRepository.GetByIdAsync(EmployeeId.Create(request.EmployeeId));

            if (employee is not null)
            {
                var jobPosition = await _jobPositionRepository.GetByIdAsync(employee.JobPositionId);

                var manager = await _employeeProfileRepository.GetByIdAsync(employee.ManagerId, manager => new
                {
                    manager.Fullname
                });

                var data = new EmployeeProfileDetailsResult(
                    employee.AccountId,
                    employee.Email,
                    employee.Fullname,
                    employee.Avatar,
                    employee.PhoneNumber,
                    employee.SSN,
                    employee.DOB,
                    employee.LivingAddress,
                    employee.Hometown,
                    employee.CvLink,
                    employee.CreatedDate,
                    employee.UpdatedDate,
                    jobPosition!.Name,
                    manager is null ? "" : manager.Fullname
                );
            }
            return new QueryResult<EmployeeProfileDetailsResult>(HttpStatusCode.NotFound, Error.EMPLOYEE_NOT_FOUND);
        }
        catch (Exception e)
        {
            return new QueryResult<EmployeeProfileDetailsResult>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}