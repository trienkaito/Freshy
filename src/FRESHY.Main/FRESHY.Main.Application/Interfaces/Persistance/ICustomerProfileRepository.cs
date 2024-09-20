using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface ICustomerProfileRepository : IRepository<Customer, CustomerId>
{
    Task<Customer?> GetCustomerByAccountId(Guid AccountId);
    Task<CustomerId?> GetCustomerIdByAccountId(Guid AccountId);
}