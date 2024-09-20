using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Entities;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IProductLikerepository : IRepository<ProductLike, ProductLikeId>
{
    Task<IEnumerable<ProductLike>?> GetProductLikesByCustomerId(CustomerId customerId);
    Task<ProductLike?> GetProductLikeByCustomerIdAndProductId(CustomerId customerId, ProductId productId);
}