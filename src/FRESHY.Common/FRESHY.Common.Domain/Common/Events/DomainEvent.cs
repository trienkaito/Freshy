using FRESHY.Common.Domain.Common.Models;
using MediatR;

namespace FRESHY.Common.Domain.Common.Events;

public class DomainEvent : INotification
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }

    public Guid AggregateId { get; set; }
}