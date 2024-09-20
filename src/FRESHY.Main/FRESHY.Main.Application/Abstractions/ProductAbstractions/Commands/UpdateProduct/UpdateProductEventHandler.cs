using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.UpdateProduct;

public class UpdateProductEventHandler : IDomainEventHandler<ProductBeingUpdated>
{
    private readonly IEventStore _eventStore;

    public UpdateProductEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(ProductBeingUpdated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}