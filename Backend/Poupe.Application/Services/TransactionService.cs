using Mapster;
using Poupe.Application.Resources;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Entities;
using Poupe.Domain.Enums;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IUnitOfWork unitOfWork, ITransactionRepository transactionRepository)
    {
        _unitOfWork = unitOfWork;
        _transactionRepository = transactionRepository;
    }

    /// <summary>
    /// Cria uma nova transação no sistema.
    /// </summary>
    /// <param name="transactionCreateDTO">Dados da transação.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o usuário ou categoria não é encontrado.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando o usuário é menor de 18 anos e a transação é do tipo Rceceita
    /// ou quando o tipo de categoria é diferente do tipo de transação.
    /// </exception>
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

            transaction.Category = category;
            transaction.User = user;

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

    /// <summary>
    /// Deleta uma transação pelo id.
    /// </summary>
    /// <param name="id">Identificador da transação.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a transação não é encontrada.
    /// </exception>
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

    /// <summary>
    /// Busca por todas as transações.
    /// </summary>
    public async Task<List<TransactionResponseDTO>> GetAllAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }

    /// <summary>
    /// Busca uma transação pelo id.
    /// </summary>
    /// <param name="id">Identificador da transação.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a transação não é encontrada.
    /// </exception>
    public async Task<TransactionResponseDTO> GetByIdAsync(Guid id)
    {
        return await _transactionRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Transação"));
    }

    /// <summary>
    /// Edita uma transação no sistema.
    /// </summary>
    /// <param name="id">Identificador da transação.</param>
    /// <param name="transactionUpdateDTO">Dados da transação.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a transação ou categoria não é encontrada.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Lançada quando o tipo de categoria é diferente do tipo de transação.
    /// </exception>
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

            transaction.Category = category;

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
