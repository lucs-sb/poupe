using Poupe.Domain.DTOs.User;

namespace Poupe.Domain.Interfaces;

public interface ITokenService
{
    Task<LoginResponseDTO> GenerateToken(Guid id);
}