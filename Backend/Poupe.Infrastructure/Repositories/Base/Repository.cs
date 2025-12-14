using Microsoft.EntityFrameworkCore;
using Poupe.Domain.Interfaces.Repositories.Base;
using Poupe.Domain.Repositories;

namespace Poupe.Infrastructure.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbContext.Set<TEntity>().FindAsync(id);

    public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
}