using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Transaction;

public record TransactionResponseDTO (string Id, string Description, decimal Value, TransactionType Type, string CategoryId, string UserId)
{
}