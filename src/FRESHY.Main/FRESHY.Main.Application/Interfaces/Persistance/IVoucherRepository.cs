using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;

namespace FRESHY.Main.Application.Interfaces.Persistance;

public interface IVoucherRepository : IRepository<Voucher, VoucherId>
{
    Task<Voucher?> CheckAndGetValidVoucherAsync(VoucherCode code);

    Task<Voucher?> GetVoucherAsync(VoucherCode voucherCode);
}