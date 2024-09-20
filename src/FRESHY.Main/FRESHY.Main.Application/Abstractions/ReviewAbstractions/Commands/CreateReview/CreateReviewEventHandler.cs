using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.CreateReview;

public class CreateReviewEventHandler : IDomainEventHandler<CustomerCreatedReview>
{
    private readonly IEventStore _eventStore;

    public CreateReviewEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(CustomerCreatedReview notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}