using Mapster;
using Poupe.Application.Resources;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ITransactionRepository transactionRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
    }

    /// <summary>
    /// Cria um novo usuário no sistema.
    /// </summary>
    /// <param name="userCreateDTO">Dados do usuário.</param>
    public async Task<UserResponseDTO> CreateAsync(UserCreateDTO userCreateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            User user = userCreateDTO.Adapt<User>();

            await _unitOfWork.Repository<User>().AddAsync(user);

            await _unitOfWork.CommitAsync();

            return user.Adapt<UserResponseDTO>();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// Deleta um usuário pelo id e todas suas transações vinculadas.
    /// </summary>
    /// <param name="id">Identificador do usuário.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o usuário não é encontrado.
    /// </exception>
    public async Task DeleteByIdAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));

            _unitOfWork.Repository<User>().Remove(user);

            await _transactionRepository.DeleteByUserId(id);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// Busca todos os usuários do sistema.
    /// </summary>
    /// <returns>Lista de usuários com dados pessoais mais despesas, receitas e saldo.</returns>
    public async Task<UserSummaryResponseDTO> GetAllAsync()
    {
        List<UserResponseDTO> userResponseDTOs = await _userRepository.GetAllAsync();

        decimal totalIncomes = userResponseDTOs.Sum(x => x.Incomes);
        decimal totalExpenses = userResponseDTOs.Sum(x => x.Expenses);
        decimal netBalance = totalIncomes - totalExpenses;

        return ValueTuple.Create(userResponseDTOs, totalIncomes, totalExpenses, netBalance).Adapt<UserSummaryResponseDTO>();
    }

    /// <summary>
    /// Busca um usuário pelo id.
    /// </summary>
    /// <param name="id">Identificador do usuário.</param>
    /// <returns>Dados pessoais do usuários mais despesas, receitas e saldo.</returns>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o usuário não é encontrado.
    /// </exception>
    public async Task<UserResponseDTO> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

    /// <summary>
    /// Edita um usuário pelo id.
    /// </summary>
    /// <param name="id">Identificador do usuário.</param>
    /// <param name="userUpdateDTO">Dados do usuário.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando o usuário não é encontrado.
    /// </exception>
    public async Task UpdateAsync(Guid id, UserUpdateDTO userUpdateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));

            userUpdateDTO.Adapt(user);

            _unitOfWork.Repository<User>().Update(user);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }
}
