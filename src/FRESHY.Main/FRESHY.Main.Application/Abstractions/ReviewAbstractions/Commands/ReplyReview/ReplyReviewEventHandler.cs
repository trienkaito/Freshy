using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Main.Domain.Models.Aggregates.ReviewAggregate.Events;
using FRESHY.SharedKernel.Extensions;
using FRESHY.SharedKernel.Interfaces;

namespace FRESHY.Main.Application.Abstractions.ReviewAbstractions.Commands.ReplyReview;

public class ReplyReviewEventHandler : IDomainEventHandler<EmployeeRepliedReview>
{
    private readonly IEventStore _eventStore;

    public ReplyReviewEventHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Handle(EmployeeRepliedReview notification, CancellationToken cancellationToken)
    {
        await _eventStore.ConvertAndSaveDomainEventToEventDocument(notification);
    }
}