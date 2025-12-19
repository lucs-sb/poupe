using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Domain.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task DeleteByUserId(Guid userId);
    new Task<List<TransactionResponseDTO>> GetAllAsync();
    new Task<TransactionResponseDTO?> GetByIdAsync(Guid id);
}
