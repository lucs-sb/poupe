using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Transaction;

public record TransactionCreateDTO (string Description, decimal Value, TransactionType Type, Guid CategoryId, Guid UserId)
{
}