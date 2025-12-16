using System.Text.Json.Serialization;

namespace Poupe.API.Models.User;

public class UpdateUserModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}
