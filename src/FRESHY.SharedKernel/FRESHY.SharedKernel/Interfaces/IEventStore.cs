using FRESHY.Common.Domain.Common.Events;

namespace FRESHY.SharedKernel.Interfaces;

public interface IEventStore
{
    Task SaveEvent(EventDocument @event);

    Task SaveRangeEvent(ICollection<EventDocument> @events);
    
    Task<IEnumerable<EventDocument>> GetEventsByType(string typeName);
}