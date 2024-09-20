using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class ProductLikeRepository : Repository<ProductLike, ProductLikeId, FreshyDbContext>, IProductLikerepository
{
    public ProductLikeRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<ProductLike?> GetProductLikeByCustomerIdAndProductId(CustomerId customerId, ProductId productId)
    {
        return await _entitySet.Where(like => like.CustomerId.Equals(customerId) && like.ProductId.Equals(productId)).FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<ProductLike>?> GetProductLikesByCustomerId(CustomerId customerId)
    {
        return await _entitySet.Where(like => like.CustomerId.Equals(customerId)).ToListAsync();
    }
}