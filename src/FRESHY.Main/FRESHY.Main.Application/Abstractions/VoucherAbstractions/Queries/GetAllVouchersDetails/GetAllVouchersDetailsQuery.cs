using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.Shared.Results;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllVouchersDetails;

public record GetAllVouchersDetailsQuery : IQuery<QueryResult<IEnumerable<AllVouchersDetailsResult>>>;

public class GetAllVouchersDetailsQueryHandler : IQueryHandler<GetAllVouchersDetailsQuery, QueryResult<IEnumerable<AllVouchersDetailsResult>>>
{
    private readonly IVoucherRepository _voucherRepository;

    public GetAllVouchersDetailsQueryHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    //TODO: CONSIDER APPLY PAGGING
    public async Task<QueryResult<IEnumerable<AllVouchersDetailsResult>>> Handle(GetAllVouchersDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var vouchers = await _voucherRepository.GetAllAsync();

            var data = vouchers.Select(voucher => new AllVouchersDetailsResult
            (
                new VoucherCodeResult(
                    voucher.VoucherCode.Value
                ),
                voucher.StartedOn,
                voucher.EndedOn,
                voucher.DiscountValue,
                voucher.Description
            )).ToList();

            return new QueryResult<IEnumerable<AllVouchersDetailsResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllVouchersDetailsResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}