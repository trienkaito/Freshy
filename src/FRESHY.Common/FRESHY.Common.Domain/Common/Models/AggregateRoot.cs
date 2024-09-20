using FRESHY.Common.Domain.Common.Events;
using FRESHY.Common.Domain.Common.Interfaces;

namespace FRESHY.Common.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvent
    where TId : notnull, ValueObject
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}