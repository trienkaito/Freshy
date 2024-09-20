using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.GetAllEmployeeProfilesQuery.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using MongoDB.Driver;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Queries.SearchEmployeeProfileQuery;

public record SearchEmployeeProfileQuery
(
    string? Fullname,
    string? HomeTown,
    string? PhoneNumber,
    int PageNumber,
    int PageSize
) : IQuery<PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>>;

public class SearchEmployeeProfileQueryHandler : IQueryHandler<SearchEmployeeProfileQuery, PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>>
{
    private readonly IEmployeeProfileRepository _employeeProfileRepository;

    public SearchEmployeeProfileQueryHandler(IEmployeeProfileRepository employeeProfileRepository)
    {
        _employeeProfileRepository = employeeProfileRepository;
    }

    public async Task<PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>> Handle(SearchEmployeeProfileQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _employeeProfileRepository.GetAllAsync(employee => new
            {
                employee.AccountId,
                employee.Id,
                employee.Fullname,
                employee.Avatar,
                employee.PhoneNumber,
                employee.SSN,
                employee.Hometown
            });

            var data = employees.Select(profile => new
                AllEmployeeProfilesResult(
                    profile.AccountId,
                    profile.Id.Value,
                    profile.Fullname,
                    profile.Avatar,
                    profile.PhoneNumber,
                    profile.SSN,
                    profile.Hometown
                ))
                .ToList()
                .Where(employee =>
                (string.IsNullOrEmpty(request.Fullname) || employee.Fullname.Contains(request.Fullname, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.HomeTown) || employee.Hometown.Contains(request.HomeTown, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || employee.PhoneNumber.Contains(request.PhoneNumber, StringComparison.OrdinalIgnoreCase)))
                .ToList();

                int totalPage = (int)Math.Ceiling((double)data.Count / request.PageSize);
            return new PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>(data, request.PageNumber, request.PageSize, totalPage);
        }
        catch (Exception e)
        {
            return new PageQueryResult<IEnumerable<AllEmployeeProfilesResult>>(request.PageSize, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}