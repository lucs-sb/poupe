using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Category;

public record CategoryResponseDTO (string Id, string Description, CategoryType Purpose)
{
}
