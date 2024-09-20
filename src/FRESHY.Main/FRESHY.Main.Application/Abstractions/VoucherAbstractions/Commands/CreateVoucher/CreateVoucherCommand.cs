using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Models.Wrappers;
using FRESHY.Main.Application.Abstractions.Shared.Commands;
using FRESHY.Main.Application.Interfaces.Persistance;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.Events;
using System.Net;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher;

public record CreateVoucherCommand
(
    VoucherCodeCommand? Code,
    string StartedOn,
    string EndedOn,
    float DiscountValue,
    string? Description,
    Guid EmployeeId
) : ICommand<CommandResult>;

public class CreateVoucherCommandHandler : ICommandHandler<CreateVoucherCommand, CommandResult>
{
    private readonly IVoucherRepository _voucherRepository;

    public CreateVoucherCommandHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<CommandResult> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _voucherRepository.UnitOfWork.BeginTransaction();

            var voucher = Voucher.Create(
                request.Code?.Value,
                Convert.ToDateTime(request.StartedOn),
                Convert.ToDateTime(request.EndedOn),
                request.DiscountValue / 100,
                request.Description
            );
/*
            var @event = new VoucherBeingGenerated(
                voucher.Id.Value,
                request.EmployeeId);

            voucher.AddDomainEvent(@event);*/

            await _voucherRepository.InsertAsync(voucher);
            await _voucherRepository.UnitOfWork.Commit(cancellationToken);

            return new CommandResult();
        }
        catch (Exception e)
        {
            return new CommandResult(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}