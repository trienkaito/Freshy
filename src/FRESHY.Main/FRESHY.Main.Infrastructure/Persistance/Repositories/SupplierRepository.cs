using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class SupplierRepository : Repository<Supplier, SupplierId, FreshyDbContext>, ISupplierRepository
{
    public SupplierRepository(FreshyDbContext context) : base(context)
    {
    }
}