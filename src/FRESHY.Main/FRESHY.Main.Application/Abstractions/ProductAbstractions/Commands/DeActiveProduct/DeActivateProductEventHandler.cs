using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.DeActiveProduct;

public class DeActivateProductEventHandler : IDomainEventHandler<ProductBeingDeActivated>
{
    private readonly IEventStore _eventStore;

    public DeActivateProductEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(ProductBeingDeActivated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}