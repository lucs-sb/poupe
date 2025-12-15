using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Transaction;

public record TransactionUpdateDTO (string Description, decimal Value, TransactionType Type, Guid CategoryId)
{
}