using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class CustomerProfileRepository : Repository<Customer, CustomerId, FreshyDbContext>, ICustomerProfileRepository
{
    public CustomerProfileRepository(FreshyDbContext context) : base(context)
    {


    }
    public async Task<Customer?> GetCustomerByAccountId(Guid AccountId)
    {
        return await _entitySet.Where(customer => customer.AccountId.Equals(AccountId)).FirstOrDefaultAsync();
    }

    public async Task<CustomerId?> GetCustomerIdByAccountId(Guid AccountId)
    {
        var customer = await _entitySet.Where(customer => customer.AccountId.Equals(AccountId)).FirstOrDefaultAsync();
        return customer?.Id;
    }
}