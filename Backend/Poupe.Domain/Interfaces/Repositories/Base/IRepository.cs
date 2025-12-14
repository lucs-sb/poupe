namespace Poupe.Domain.Interfaces.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    void Remove(TEntity entity);
}