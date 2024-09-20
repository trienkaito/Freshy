using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeeProfilesQuery.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeesQuery;

public record GetAllEmployeeProfilesQuery
(
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>>;

public class GetAllEmployeeQueryHandler : IQueryHandler<GetAllEmployeeProfilesQuery, PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>>
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public GetAllEmployeeQueryHandler(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>> Handle(GetAllEmployeeProfilesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _employeeProfileRepository.GetByPagingAsync(request.PageNumber, request.PageSize, employee => new
            {
                employee.AccountId,
                employee.Id,
                employee.Fullname,
                employee.Avatar,
                employee.PhoneNumber,
                employee.Hometown,
                employee.SSN
                
            });

            var allEmployees = await _employeeProfileRepository.GetAllAsync(employee => new
            {
                employee.Id
            });

            int totalPage = (int)Math.Ceiling((double)allEmployees.Count() / request.PageSize);

            var data = employees.Select(profile =>
            new AllEmployeeProfilesResult(
                profile.AccountId,
                profile.Id.Value,
                profile.Fullname,
                profile.Avatar,
                profile.PhoneNumber,
                profile.SSN,
                profile.Hometown
            )).ToList();

            return new PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}