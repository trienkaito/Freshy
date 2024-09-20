using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface ISupplierRepository : IRepository<Supplier, SupplierId>
{
}