using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poupe.API.Models.User;
using Poupe.API.Utils;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Interfaces;

namespace Poupe.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserModel createUserModel)
    {
        UserCreateDTO userCreateDTO = createUserModel.Adapt<UserCreateDTO>();

        UserResponseDTO userResponseDTO = await _userService.CreateAsync(userCreateDTO);

        return Ok(userResponseDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        UserResponseDTO userResponseDTO = await _userService.GetByIdAsync(identifier);

        return Ok(userResponseDTO);
    }

    [HttpGet]
    public async Task<IActionResult> GetByAllAsync()
    {
        List<UserResponseDTO> userResponseDTOs = await _userService.GetAllAsync();

        return Ok(userResponseDTOs);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserByIdAsync([FromRoute(Name = "id")] string id, [FromBody] UpdateUserModel updateUserModel)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        UserUpdateDTO userUpdateDTO = updateUserModel.Adapt<UserUpdateDTO>();

        await _userService.UpdateAsync(identifier, userUpdateDTO);

        return Accepted();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdentifierAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        await _userService.DeleteByIdAsync(identifier);

        return NoContent();
    }
}
