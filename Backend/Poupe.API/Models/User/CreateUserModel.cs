using System.Text.Json.Serialization;

namespace Poupe.API.Models.User;

public class CreateUserModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }
}