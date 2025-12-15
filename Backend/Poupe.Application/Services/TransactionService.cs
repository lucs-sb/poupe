using Mapster;
using Poupe.Application.Resources;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Entities;
using Poupe.Domain.Enums;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionResponseDTO> CreateAsync(TransactionCreateDTO transactionCreateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(transactionCreateDTO.CategoryId) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

            if (category.Purpose != CategoryType.Both && transactionCreateDTO.Type.ToString() != category.Purpose.ToString())
                throw new InvalidOperationException(BusinessMessage.TransactionType_Error);
            
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(transactionCreateDTO.UserId) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));

            int legalAge = 18;

            if (user.Age < legalAge && transactionCreateDTO.Type != TransactionType.Expense)
                throw new InvalidOperationException(BusinessMessage.LegalAge_Error);

            Transaction transaction = transactionCreateDTO.Adapt<Transaction>();

            await _unitOfWork.Repository<Transaction>().AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return transaction.Adapt<TransactionResponseDTO>();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Transaction transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Transação"));

            _unitOfWork.Repository<Transaction>().Remove(transaction);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task<List<TransactionResponseDTO>> GetAllAsync()
    {
        List<Transaction> transactions = await _unitOfWork.Repository<Transaction>().GetAllAsync();

        return transactions.Adapt<List<TransactionResponseDTO>>();
    }

    public async Task<TransactionResponseDTO> GetByIdAsync(Guid id)
    {
        Transaction transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Transação"));

        return transaction.Adapt<TransactionResponseDTO>();
    }

    public async Task UpdateAsync(Guid id, TransactionUpdateDTO transactionUpdateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(transactionUpdateDTO.CategoryId) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

            if (category.Purpose != CategoryType.Both && transactionUpdateDTO.Type.ToString() != category.Purpose.ToString())
                throw new InvalidOperationException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

            Transaction transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Transação"));

            transactionUpdateDTO.Adapt(transaction);

            _unitOfWork.Repository<Transaction>().Update(transaction);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }
}
