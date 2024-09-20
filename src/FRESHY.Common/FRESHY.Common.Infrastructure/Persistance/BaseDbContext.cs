using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Common.Domain.Common.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FRESHY.Common.Infrastructure.Persistance;

public class BaseDbContext<TContext> : DbContext, IUnitOfWork where TContext : BaseDbContext<TContext>
{
    private readonly EventInterceptor _eventInterceptor;
    private IDbContextTransaction _transaction = null!;

    public BaseDbContext(
        DbContextOptions<TContext> options,
        EventInterceptor eventInterceptor) : base(options)
    {
        _eventInterceptor = eventInterceptor;
    }

    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
            _transaction.Commit();
        }
        catch
        {
            Rollback();
        }
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_eventInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<DomainEvent>();

        base.OnModelCreating(modelBuilder);
    }
}