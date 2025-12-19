using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Category;

public record CategoryResponseDTO (Guid Id, string Description, CategoryType Purpose)
{
}
