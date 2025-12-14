namespace Poupe.Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork : IDisposable
{
    ValueTask BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
}