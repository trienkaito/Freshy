using FRESHY.Common.Domain.Common.Events;
using FRESHY.SharedKernel.Interfaces;
using Newtonsoft.Json;

namespace FRESHY.SharedKernel.Extensions;

public static class EventStoreExtensions
{
    public static async Task ConvertAndSaveDomainEventToEventDocument(this IEventStore eventStore, DomainEvent @event)
    {
        var docs = new EventDocument
        {
            Id = Guid.NewGuid(),
            AggregateId = @event.AggregateId,
            Type = @event.GetType().Name,
            OccurredOn = DateTime.UtcNow,
            Content = JsonConvert.SerializeObject(@event,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    })
        };
        await eventStore.SaveEvent(docs);
    }
}