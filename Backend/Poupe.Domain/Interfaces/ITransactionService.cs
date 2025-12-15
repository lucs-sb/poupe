using Poupe.Domain.DTOs.Transaction;

namespace Poupe.Domain.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponseDTO> CreateAsync(TransactionCreateDTO transactionCreateDTO);
    Task<TransactionResponseDTO> GetByIdAsync(Guid id);
    Task<List<TransactionResponseDTO>> GetAllAsync();
    Task UpdateAsync(Guid id, TransactionUpdateDTO transactionUpdateDTO);
    Task DeleteByIdAsync(Guid id);
}