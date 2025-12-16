using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poupe.API.Models.Category;
using Poupe.API.Utils;
using Poupe.Domain.DTOs.Category;
using Poupe.Domain.Interfaces;

namespace Poupe.API.Controllers;

[ApiController]
[Route("api/category")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryModel createCategoryModel)
    {
        CategoryCreateDTO categoryCreateDTO = createCategoryModel.Adapt<CategoryCreateDTO>();

        CategoryResponseDTO categoryResponseDTO = await _categoryService.CreateAsync(categoryCreateDTO);

        return Ok(categoryResponseDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        CategoryResponseDTO categoryResponseDTO = await _categoryService.GetByIdAsync(identifier);

        return Ok(categoryResponseDTO);
    }

    [HttpGet]
    public async Task<IActionResult> GetByAllAsync()
    {
        List<CategoryResponseDTO> categoryResponseDTOs = await _categoryService.GetAllAsync();

        return Ok(categoryResponseDTOs);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryByIdAsync([FromRoute(Name = "id")] string id, [FromBody] UpdateCategoryModel updateCategoryModel)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        CategoryUpdateDTO categoryUpdateDTO = updateCategoryModel.Adapt<CategoryUpdateDTO>();

        await _categoryService.UpdateAsync(identifier, categoryUpdateDTO);

        return Accepted();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdentifierAsync([FromRoute(Name = "id")] string id)
    {
        Util.ValidateGuid(id);

        Guid identifier = Guid.Parse(id);

        await _categoryService.DeleteByIdAsync(identifier);

        return NoContent();
    }
}
