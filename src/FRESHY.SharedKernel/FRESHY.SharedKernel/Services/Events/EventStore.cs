using FRESHY.Common.Domain.Common.Events;
using FRESHY.SharedKernel.Interfaces;
using FRESHY.SharedKernel.Persistance;
using MongoDB.Driver;

namespace FRESHY.SharedKernel.Services.Events;

public class EventStore : IEventStore
{
    private readonly IMongoCollection<EventDocument> _eventCollection;

    public EventStore(EventSourcingDbContext dbContext)
    {
        _eventCollection = dbContext.Events;
    }

    public async Task<IEnumerable<EventDocument>> GetEventsByType(string typeName)
    {
        var filter = Builders<EventDocument>.Filter.Eq("Type", typeName);
        var events = await _eventCollection.Find(filter).ToListAsync();
        return events;
    }

    public async Task SaveEvent(EventDocument @event)
    {
        await _eventCollection.InsertOneAsync(@event);
    }

    public async Task SaveRangeEvent(ICollection<EventDocument> events)
    {
        await _eventCollection.InsertManyAsync(events);
    }
}