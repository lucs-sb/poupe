using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Poupe.Infrastructure.Auth;

public class TokenService : ITokenService
{
    private readonly IOptions<AuthSettings> _authSettings;

    public TokenService(IOptions<AuthSettings> authSettings) => _authSettings = authSettings;

    public Task<LoginResponseDTO> GenerateToken(Guid id)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, id.ToString())
        ];

        DateTime expires = DateTime.UtcNow.AddMinutes(_authSettings.Value.AccessTokenExpirationMinutes);
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_authSettings.Value.SecretKey));
        JwtSecurityToken tokenData = new(
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            claims: claims,
            expires: expires);

        string token = new JwtSecurityTokenHandler().WriteToken(tokenData);

        return Task.FromResult(new LoginResponseDTO(token, expires));
    }
}