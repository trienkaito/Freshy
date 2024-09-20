using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IEmployeeProfileRepository : IRepository<Employee, EmployeeId>
{
}