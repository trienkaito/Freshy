using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ProductRepository : Repository<Product, ProductId, FreshyDbContext>, IProductRepository
{
    public ProductRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TResult?>> GetProductsBySupplierIdAsync<TResult>(SupplierId supplierId, Expression<Func<Product, TResult>> selector)
    {
        var products = await _entitySet.Where(product => product.SupplierId.Equals(supplierId))
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
        return products;
    }
}