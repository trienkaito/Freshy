using FRESHY.Common.Application.Interfaces.Persistance;
using FRESHY.Common.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FRESHY.Common.Infrastructure.Persistance;

public abstract class Repository<TAggregate, TId, TContext> : IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : ValueObject
    where TContext : BaseDbContext<TContext>
{
    protected readonly BaseDbContext<TContext> _context;
    protected readonly DbSet<TAggregate> _entitySet;

    protected Repository(BaseDbContext<TContext> context)
    {
        _context = context;
        _entitySet = context.Set<TAggregate>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Delete(TAggregate entity)
    {
        _entitySet.Remove(entity);
    }

    public async Task<IEnumerable<TAggregate>> GetAllAsync()
    {
        return await _entitySet.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TAggregate, TResult>> selector)
    {
        return await _entitySet.AsNoTracking().Select(selector).ToListAsync();
    }

    public async Task<TResult?> GetByIdAsync<TResult>(TId? id, Expression<Func<TAggregate, TResult>> selector)
    {
        return await _entitySet
        .AsNoTracking()
        .Where(aggregate => aggregate.Id.Equals(id))
        .Select(selector)
        .FirstOrDefaultAsync();
    }

    public async Task<TAggregate?> GetByIdAsync(TId? id)
    {
        return await _entitySet.FirstOrDefaultAsync(aggregate => aggregate.Id.Equals(id));
    }

    public async Task<IEnumerable<TResult>> GetByPagingAsync<TResult>(int pageNumber, int pageSize, Expression<Func<TAggregate, TResult>> selector)
    {
        return await _entitySet
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync();
    }

    public async Task InsertAsync(TAggregate entity)
    {
        await _entitySet.AddAsync(entity);
    }

    public async Task InsertRange(IEnumerable<TAggregate> entitiesToAdd)
    {
        await _entitySet.AddRangeAsync(entitiesToAdd);
    }

    public void RemoveRange(IEnumerable<TAggregate> entitiesToRemove)
    {
        _entitySet.RemoveRange(entitiesToRemove);
    }

    public void Update(TAggregate entity)
    {
        _entitySet.Update(entity);
    }
}