using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IProductUnitRepository : IRepository<ProductUnit, ProductUnitId>
{
    Task<IEnumerable<ProductUnit>?> GetProductUnitsByProductId(ProductId productId);

    Task<bool> CheckProductUnitQuantity(ProductUnitId productUnitId, int boughtQuantity);
}