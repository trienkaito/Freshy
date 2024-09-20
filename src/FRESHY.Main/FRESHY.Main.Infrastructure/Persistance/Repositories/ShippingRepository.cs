using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ShippingRepository : Repository<Shipping, ShippingId, FreshyDbContext>, IShippingRepository
{
    public ShippingRepository(FreshyDbContext context) : base(context)
    {
    }
}