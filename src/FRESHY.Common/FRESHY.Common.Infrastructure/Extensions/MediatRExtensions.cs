using FRESHY.Common.Domain.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FRESHY.Common.Infrastructure.Extensions;

public static class MediatRExtensions
{
    public static async Task PublishDomainEvents(this IPublisher publisher, DbContext context)
    {
        if (context is null)
        {
            return;
        }

        var aggregatesWithDomainEvents = context.ChangeTracker.Entries<IHasDomainEvent>()
                                        .Where(entry => entry.Entity.DomainEvents.Any())
                                        .Select(entry => entry.Entity)
                                        .ToList();
        var @events = aggregatesWithDomainEvents.SelectMany(entity => entity.DomainEvents).ToList();

        if (@events.Count == 0)
        {
            return;
        }

        aggregatesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

        foreach (var @event in @events)
        {
            await publisher.Publish(@event);
        }
    }
}