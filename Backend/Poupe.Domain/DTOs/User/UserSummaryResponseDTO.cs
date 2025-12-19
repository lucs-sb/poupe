namespace Poupe.Domain.DTOs.User;

public record UserSummaryResponseDTO (List<UserResponseDTO> Users, decimal TotalIncomes, decimal TotalExpenses, decimal NetBalance)
{
}
