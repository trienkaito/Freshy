using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOfflineOrder;

public class CreateOfflineOrderEventHandler : IDomainEventHandler<OfflineOrderCreated>
{
    private readonly IEventStore _eventStore;

    public CreateOfflineOrderEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(OfflineOrderCreated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}