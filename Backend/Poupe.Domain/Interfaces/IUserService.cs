using Poupe.Domain.DTOs.User;

namespace Poupe.Domain.Interfaces;

public interface IUserService
{
    Task<UserResponseDTO> CreateAsync(UserCreateDTO userCreateDTO);
    Task<UserResponseDTO> GetByIdAsync(Guid id);
    Task<List<UserResponseDTO>> GetAllAsync();
    Task UpdateAsync(Guid id, UserUpdateDTO userUpdateDTO);
    Task DeleteByIdAsync(Guid id);
}