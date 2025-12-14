using Poupe.Domain.Enums;

namespace Poupe.Domain.DTOs.Category;

public record CategoryUpdateDTO (string Description, CategoryType Purpose)
{
}