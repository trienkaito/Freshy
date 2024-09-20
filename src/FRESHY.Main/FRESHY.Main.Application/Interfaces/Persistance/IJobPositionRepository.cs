using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IJobPositionRepository : IRepository<JobPosition, JobPositionId>
{
}