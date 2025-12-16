using Poupe.Domain.DTOs.User;

namespace Poupe.Domain.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
}