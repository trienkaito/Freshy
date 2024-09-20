using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ProductTypeRepository : Repository<ProductType, ProductTypeId, FreshyDbContext>, IProductTypeRepository
{
    public ProductTypeRepository(FreshyDbContext context) : base(context)
    {
    }
}