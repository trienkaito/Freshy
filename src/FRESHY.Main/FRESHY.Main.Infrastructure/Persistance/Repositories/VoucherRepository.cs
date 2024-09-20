using FRESHY.Common.Domain.Common.Models;
using FRESHY.Common.Infrastructure.Persistance;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Main.Infrastructure.Persistance.Repositories;

public class VoucherRepository : Repository<Voucher, VoucherId, FreshyDbContext>, IVoucherRepository
{
    public VoucherRepository(FreshyDbContext context) : base(context)
    {
    }

    public async Task<Voucher?> CheckAndGetValidVoucherAsync(VoucherCode code)
    {
        var voucher = await GetVoucherAsync(code);

        if (voucher is not null)
        {
            if (voucher.StartedOn.CompareTo(DateTime.UtcNow) <=0 && voucher.EndedOn.CompareTo(DateTime.UtcNow) > 0) return voucher;

            return null;
        }

        return null;
    }

    public async Task<Voucher?> GetVoucherAsync(VoucherCode code)
    {
        return await _entitySet.FirstOrDefaultAsync(voucher => voucher.VoucherCode.Value == code.Value);
    }
}