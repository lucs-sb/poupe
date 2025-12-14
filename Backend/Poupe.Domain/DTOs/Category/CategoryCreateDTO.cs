using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Category;

public record CategoryCreateDTO (string Description, CategoryType Purpose)
{
}