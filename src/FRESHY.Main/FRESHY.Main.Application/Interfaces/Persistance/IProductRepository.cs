using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;
using System.Linq.Expressions;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<IEnumerable<TResult?>> GetProductsBySupplierIdAsync<TResult>(SupplierId supplierId, Expression<Func<Product, TResult>> selector);
}