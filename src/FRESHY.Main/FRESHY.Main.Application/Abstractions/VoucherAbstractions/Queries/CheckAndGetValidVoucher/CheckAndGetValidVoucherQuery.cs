using System.Net;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CheckAndGetValidVoucher;

public record CheckAndGetValidVoucherQuery
(
    string Code

) : ICommand<QueryResult<double>>;

public class CheckAndGetValidVoucherQueryHandler : ICommandHandler<CheckAndGetValidVoucherQuery, QueryResult<double>>
{
    private readonly IVoucherRepository _voucherRepository;

    public CheckAndGetValidVoucherQueryHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<QueryResult<double>> Handle(CheckAndGetValidVoucherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _voucherRepository.UnitOfWork.BeginTransaction();

            var code = request.Code;

            if (code is null)
            {
                return new QueryResult<double>(0);
            }
            var voucher = await _voucherRepository.CheckAndGetValidVoucherAsync(VoucherCode.Create(code));

            if (voucher is null)
            {
                return new QueryResult<double>(0);
            }

            await _voucherRepository.UnitOfWork.Commit(cancellationToken);
            return new QueryResult<double>(voucher.DiscountValue);
        }
        catch (Exception e)
        {
            return new QueryResult<double>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}