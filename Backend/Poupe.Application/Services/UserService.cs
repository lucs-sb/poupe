using Mapster;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id) ?? throw new KeyNotFoundException("Usuário não encontrado");

            _unitOfWork.Repository<User>().Remove(user);

            //TODO: Remover todas a transações dessa pessoa

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    public async Task<List<UserResponseDTO>> GetAllAsync()
    {
        List<User> users = await _unitOfWork.Repository<User>().GetAllAsync();

        //TODO exibir o total de receitas, despesas e o saldo (receita – despesa) de cada pessoa

        return users.Adapt<List<UserResponseDTO>>();
    }

    public async Task<UserResponseDTO> GetByIdAsync(Guid id)
    {
        User user = await _unitOfWork.Repository<User>().GetByIdAsync(id) ?? throw new KeyNotFoundException("Usuário não encontrado");

        //TODO exibir o total de receitas, despesas e o saldo (receita – despesa)

        return user.Adapt<UserResponseDTO>();
    }

    public async Task UpdateAsync(Guid id, UserUpdateDTO userUpdateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            User user = await _unitOfWork.Repository<User>().GetByIdAsync(id) ?? throw new KeyNotFoundException("Usuário não encontrado");

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
