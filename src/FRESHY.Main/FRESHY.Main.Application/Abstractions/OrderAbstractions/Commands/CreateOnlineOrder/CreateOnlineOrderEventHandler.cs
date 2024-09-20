using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.OrderDetailAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.OrderAbstractions.Commands.CreateOnlineOrder;

public class CreateOnlineOrderEventHandler : IDomainEventHandler<OnlineOrderCreated>
{
    private readonly IEventStore _eventStore;

    public CreateOnlineOrderEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(OnlineOrderCreated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}