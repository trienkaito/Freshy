using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ShippingAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IShippingRepository : IRepository<Shipping, ShippingId>
{
}