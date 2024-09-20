namespace FRESHY.Common.Application.Interfaces.Persistance;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void BeginTransaction();

    Task Commit(CancellationToken cancellationToken = default);

    void Rollback();
}