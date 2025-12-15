using Microsoft.EntityFrameworkCore;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Repositories;
using Poupe.Infrastructure.Repositories.Base;

namespace Poupe.Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task DeleteByUserId(Guid userId)
    {
        await _dbContext.Transactions.Where(t => t.UserId == userId)
            .ExecuteDeleteAsync();
    }
}
