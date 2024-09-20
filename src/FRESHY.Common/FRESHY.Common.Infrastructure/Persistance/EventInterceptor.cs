using FRESHY.Common.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FRESHY.Common.Infrastructure.Persistance;

public class EventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public EventInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        _publisher.PublishDomainEvents(eventData.Context!).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await _publisher.PublishDomainEvents(eventData.Context!);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}