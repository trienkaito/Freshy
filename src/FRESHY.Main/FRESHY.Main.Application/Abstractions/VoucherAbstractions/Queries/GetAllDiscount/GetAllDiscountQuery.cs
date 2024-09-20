using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.Shared.Results;
using FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllDiscount.Results;
using FRESHY.Main.Application.Interfaces.Persistance;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Queries.GetAllDiscount;

public record GetAllDiscountQuery : IQuery<QueryResult<IEnumerable<AllDiscountResult>>>;

public class GetAllDiscountQueryHandler : IQueryHandler<GetAllDiscountQuery, QueryResult<IEnumerable<AllDiscountResult>>>
{
    private readonly IVoucherRepository _voucherRepository;

    public GetAllDiscountQueryHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<QueryResult<IEnumerable<AllDiscountResult>>> Handle(GetAllDiscountQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var discounts = await _voucherRepository.GetAllAsync(discount => new
            {
                discount.Id,
                discount.VoucherCode,
                discount.EndedOn,
                discount.DiscountValue
            });

            var discountsOnly = discounts.Where(discount => discount.EndedOn.CompareTo(DateTime.MaxValue) == 0).ToList();

            var data = discountsOnly.Select(discount =>
            {
                return new AllDiscountResult(
                    new VoucherCodeResult(
                        discount.VoucherCode.Value
                    ),
                    discount.DiscountValue
                );
            }).ToList();

            return new QueryResult<IEnumerable<AllDiscountResult>>(data);
        }
        catch (Exception e)
        {
            return new QueryResult<IEnumerable<AllDiscountResult>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}