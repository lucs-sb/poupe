using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    new Task<List<UserResponseDTO>> GetAllAsync();
    new Task<UserResponseDTO?> GetByIdAsync(Guid id);
}
