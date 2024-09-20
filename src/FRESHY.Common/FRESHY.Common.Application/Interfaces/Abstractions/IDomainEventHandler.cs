using FRESHY.Common.Domain.Common.Events;
using MediatR;

namespace FRESHY.Common.Application.Interfaces.Abstractions;

public interface IDomainEventHandler<TDomain> : INotificationHandler<TDomain>
    where TDomain : DomainEvent
{
}