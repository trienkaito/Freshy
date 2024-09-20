using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Queries.GetAllJobPositions.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using MongoDB.Driver;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.JobPositionAbstractions.Queries.GetAllJobPositions;

public record GetAllJobPosistionsQuery : IQuery<QueryResult<IEnumerable<AllJobPositionsResult>>>;

public class GetAllJobPosistionsQueryHandler : IQueryHandler<GetAllJobPosistionsQuery, QueryResult<IEnumerable<AllJobPositionsResult>>>
{
    private readonly IJobPositionRepository _jobPositionRepository;

    public GetAllJobPosistionsQueryHandler(IJobPositionRepository jobPositionRepository)
    {
        _jobPositionRepository = jobPositionRepository;
    }

    public async Task<QueryResult<IEnumerable<AllJobPositionsResult>>> Handle(GetAllJobPosistionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var positions = await _jobPositionRepository.GetAllAsync(position => new
            {
                position.Id,
                position.Name,
                position.Description,
                position.Salary
            });

            var data = positions.Select(position =>
            new AllJobPositionsResult(
                position.Id.Value,
                position.Name,
                position.Description,
                position.Salary
                )
            ).ToList();

            return new QueryResult<IEnumerable<AllJobPositionsResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllJobPositionsResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}