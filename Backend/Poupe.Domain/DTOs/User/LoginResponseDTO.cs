namespace Poupe.Domain.DTOs.User;

public record LoginResponseDTO(string access_token, DateTime expiration)
{
}