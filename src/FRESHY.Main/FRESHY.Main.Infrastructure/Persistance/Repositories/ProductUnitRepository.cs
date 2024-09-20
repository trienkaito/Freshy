using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ProductUnitRepository : Repository<ProductUnit, ProductUnitId, FreshyDbContext>, IProductUnitRepository
{
    public ProductUnitRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckProductUnitQuantity(ProductUnitId productUnitId, int boughtQuantity)
    {
        var unit = await GetByIdAsync(productUnitId);
        return unit is not null && (unit.Quantity >= boughtQuantity);
    }

    public async Task<IEnumerable<ProductUnit>?> GetProductUnitsByProductId(ProductId productId)
    {
        var units = await _entitySet.Where(unit => unit.ProductId.Equals(productId))
            .ToListAsync();
        return units;
    }
}