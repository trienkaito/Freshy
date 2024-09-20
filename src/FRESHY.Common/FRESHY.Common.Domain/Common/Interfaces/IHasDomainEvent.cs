using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.Common.Domain.Common.Interfaces;

public interface IHasDomainEvent
{
    public IReadOnlyList<DomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}