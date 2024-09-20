using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductTypeAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IProductTypeRepository : IRepository<ProductType, ProductTypeId>
{
}