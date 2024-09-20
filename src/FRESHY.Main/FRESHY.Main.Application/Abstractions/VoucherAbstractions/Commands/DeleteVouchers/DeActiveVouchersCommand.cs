using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.ValueObjects;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.DeleteVouchers;

public record DeActiveVouchersCommand
(
    Guid EmployeeId,
    Guid VoucherId
) : ICommand<CommandResult>;

public class DeleteVouchersCommandHandler : ICommandHandler<DeActiveVouchersCommand, CommandResult>
{
    private readonly IVoucherRepository _voucherRepository;

    public DeleteVouchersCommandHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    //TODO: Implement and optimize this
    public async Task<CommandResult> Handle(DeActiveVouchersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _voucherRepository.UnitOfWork.BeginTransaction();

            var voucher = await _voucherRepository.GetByIdAsync(VoucherId.Create(request.VoucherId));

            if (voucher is not null)
            {
                voucher.DeActivateVoucher();
                await _voucherRepository.UnitOfWork.Commit(cancellationToken);
                return new CommandResult();
            }
            return new CommandResult(HttpStatusCode.NotFound, null);
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}