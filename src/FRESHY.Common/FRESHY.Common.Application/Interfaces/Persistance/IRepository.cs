using FRESHY.Common.Domain.Common.Models;
using System.Linq.Expressions;

namespace FRESHY.Common.Application.Interfaces.Persistance;

public interface IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : ValueObject
{
    IUnitOfWork UnitOfWork { get; }

    Task<IEnumerable<TAggregate>> GetAllAsync();

    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TAggregate, TResult>> selector);

    Task<IEnumerable<TResult>> GetByPagingAsync<TResult>(int pageNumber, int pageSize, Expression<Func<TAggregate, TResult>> selector);

    Task<TAggregate?> GetByIdAsync(TId? id);

    Task<TResult?> GetByIdAsync<TResult>(TId? id, Expression<Func<TAggregate, TResult>> selector);

    Task InsertRange(IEnumerable<TAggregate> entitiesToAdd);

    Task InsertAsync(TAggregate entity);

    void Update(TAggregate entity);

    void Delete(TAggregate entity);

    void RemoveRange(IEnumerable<TAggregate> entitiesToRemove);
}