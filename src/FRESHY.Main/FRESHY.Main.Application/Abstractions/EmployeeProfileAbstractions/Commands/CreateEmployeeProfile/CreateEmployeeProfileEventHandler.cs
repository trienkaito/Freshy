using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.EmployeeAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.EmployeeProfileAbstractions.Commands.CreateEmployeeProfile;

public class CreateEmployeeProfileEventHandler : IDomainEventHandler<EmployeeProfileCreated>
{
    private readonly IEventStore _eventStore;

    public CreateEmployeeProfileEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(EmployeeProfileCreated notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}