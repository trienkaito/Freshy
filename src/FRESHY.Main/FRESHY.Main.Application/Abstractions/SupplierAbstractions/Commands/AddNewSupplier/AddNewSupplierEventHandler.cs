using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.SupplierAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.SupplierAbstractions.Commands.AddNewSupplier;

public class AddNewSupplierEventHandler : IDomainEventHandler<SupplierAdded>
{
    private readonly IEventStore _eventStore;

    public AddNewSupplierEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(SupplierAdded notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}