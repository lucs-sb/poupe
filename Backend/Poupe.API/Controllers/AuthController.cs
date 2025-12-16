using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poupe.API.Models.User;
using Poupe.Application.Services;
using Poupe.Domain.DTOs.User;

namespace Poupe.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        LoginDTO loginDTO = loginModel.Adapt<LoginDTO>();

        LoginResponseDTO loginResponseDTO = await _authService.LoginAsync(loginDTO);

        return Ok(loginResponseDTO);
    }
}