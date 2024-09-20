using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class EmployeeProfileRepository : Repository<Employee, EmployeeId, FreshyDbContext>, IEmployeeProfileRepository
{
    public EmployeeProfileRepository(FreshyDbContext context) : base(context)
    {
    }
}