using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Poupe.API.Models.User;

public class LoginModel
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}