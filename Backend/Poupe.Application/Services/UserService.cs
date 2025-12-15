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

    public async Task<UserGetAllResponseDTO> GetAllAsync()
    {
        List<UserResponseDTO> userResponseDTOs = await _userRepository.GetAllAsync();

        decimal totalIncomes = userResponseDTOs.Sum(x => x.Incomes);
        decimal totalExpenses = userResponseDTOs.Sum(x => x.Expenses);
        decimal netBalance = totalIncomes - totalExpenses;

        return ValueTuple.Create(userResponseDTOs, totalIncomes, totalExpenses, netBalance).Adapt<UserGetAllResponseDTO>();
    }

    public async Task<UserResponseDTO> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

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
