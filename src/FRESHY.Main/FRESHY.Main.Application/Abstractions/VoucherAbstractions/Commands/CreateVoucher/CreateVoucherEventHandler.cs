using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.VoucherAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.VoucherAbstractions.Commands.CreateVoucher;

public class CreateVoucherEventHandler : IDomainEventHandler<VoucherBeingGenerated>
{
    private readonly IEventStore _eventStore;

    public CreateVoucherEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(VoucherBeingGenerated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}