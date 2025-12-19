using Microsoft.EntityFrameworkCore;
using Poupe.Domain.DTOs.Category;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.DTOs.User;
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
        await _dbContext.Transactions.Where(t => t.User.Id == userId)
            .ExecuteDeleteAsync();
    }

    public new async Task<List<TransactionResponseDTO>> GetAllAsync()
    {
        return await _dbContext.Transactions
            .Select(t => new TransactionResponseDTO(
                t.Id!.Value,
                t.Description,
                t.Value,
                t.Type,
                new CategoryResponseDTO(
                    t.Category.Id!.Value,
                    t.Category.Description,
                    t.Category.Purpose
                ),
                new UserResponseDTO(
                    t.User.Id!.Value,
                    t.User.Name,
                    t.User.Age,
                    0,
                    0,
                    0
                )
            ))
            .ToListAsync();
    }

    public async Task<TransactionResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Transactions
            .Where(t => t.Id == id)
            .Select(t => new TransactionResponseDTO(
                t.Id!.Value,
                t.Description,
                t.Value,
                t.Type,
                new CategoryResponseDTO(
                    t.Category.Id!.Value,
                    t.Category.Description,
                    t.Category.Purpose
                ),
                new UserResponseDTO(
                    t.User.Id!.Value,
                    t.User.Name,
                    t.User.Age,
                    0,
                    0,
                    0
                )
            ))
            .FirstOrDefaultAsync();
    }
}
