namespace Poupe.Domain.Settings;

public class AuthSettings
{
    public string SecretKey { get; set; } = default!;
    public int AccessTokenExpirationMinutes { get; set; }
}