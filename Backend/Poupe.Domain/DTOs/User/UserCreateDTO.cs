namespace Poupe.Domain.DTOs.User;

public record UserCreateDTO (string Name, int Age, string Email, string Password)
{
}