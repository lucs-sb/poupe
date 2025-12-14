using Poupe.Domain.Enums;
using System.Text.Json.Serialization;

namespace Poupe.API.Models.Category;

public class CreateCategoryModel
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("purpose")]
    public CategoryType? Purpose { get; set; }
}