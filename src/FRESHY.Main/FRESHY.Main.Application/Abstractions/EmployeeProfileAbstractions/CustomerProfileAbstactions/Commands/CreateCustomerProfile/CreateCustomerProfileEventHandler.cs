using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.CustomerAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.CustomerProfileAbstactions.Commands.CreateCustomerProfile;

public class CreateCustomerProfileEventHandler : IDomainEventHandler<CustomerProfileCreated>
{
    private readonly IEventStore _eventStore;

    public CreateCustomerProfileEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(CustomerProfileCreated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}