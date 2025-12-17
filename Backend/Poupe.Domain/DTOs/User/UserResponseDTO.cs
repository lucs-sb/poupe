namespace Poupe.Domain.DTOs.User;

public record UserResponseDTO(Guid Id, string Name, int Age, decimal Incomes, decimal Expenses, decimal Balance)
{
}