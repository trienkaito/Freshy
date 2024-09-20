using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Abstractions.Shared.Commands;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateDiscountForOffline;

public record CreateDiscountForOfflineCommand
(
    VoucherCodeCommand Code,
    float DiscountValue,
    string? Description,
    Guid EmployeeId
) : ICommand<CommandResult>;

public class CreateDiscountForOfflineCommandHandler : ICommandHandler<CreateDiscountForOfflineCommand, CommandResult>
{
    private readonly IVoucherRepository _voucherRepository;

    public CreateDiscountForOfflineCommandHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<CommandResult> Handle(CreateDiscountForOfflineCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _voucherRepository.UnitOfWork.BeginTransaction();

            var discount = Voucher.CreateDiscountForOffline(
                request.Code.Value,
                request.DiscountValue / 100,
                request.Description
            );

            await _voucherRepository.InsertAsync(discount);
            await _voucherRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}