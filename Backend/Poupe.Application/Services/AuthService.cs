using Microsoft.AspNetCore.Identity;
using Poupe.Application.Resources;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories;

namespace Poupe.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        try
        {
            User user = await _userRepository.GetByEmailAsync(loginDTO.Email) ?? throw new InvalidOperationException(BusinessMessage.Unauthorized_Warning);

            if (_passwordHasher.VerifyHashedPassword(user, user.Password!, loginDTO.Password) is PasswordVerificationResult.Failed)
                throw new InvalidOperationException(BusinessMessage.Unauthorized_Warning);

            return await _tokenService.GenerateToken(user.Id!.Value);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException();
        }
    }
}