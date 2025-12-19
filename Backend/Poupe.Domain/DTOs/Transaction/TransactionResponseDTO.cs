using Poupe.Domain.DTOs.Category;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Transaction;

public record TransactionResponseDTO (Guid Id, string Description, decimal Value, TransactionType Type, CategoryResponseDTO? Category, UserResponseDTO? User)
{
}