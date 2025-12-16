namespace Poupe.Domain.DTOs.User;

public record UserResponseDTO(Guid Id, string Name, int Age, string Email, decimal Incomes, decimal Expenses, decimal Balance)
{
}