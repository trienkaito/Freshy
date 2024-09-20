using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate;
using FRESHY.Main.Domain.Models.Aggregates.JobPositionAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class JobPositionRepository : Repository<JobPosition, JobPositionId, FreshyDbContext>, IJobPositionRepository
{
    public JobPositionRepository(FreshyDbContext context) : base(context)
    {
    }
}