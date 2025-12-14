using Poupe.Domain.DTOs.Category;

namespace Poupe.Domain.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDTO> CreateAsync(CategoryCreateDTO CategoryCreateDTO);
    Task<CategoryResponseDTO> GetByIdAsync(Guid id);
    Task<List<CategoryResponseDTO>> GetAllAsync();
    Task UpdateAsync(Guid id, CategoryUpdateDTO CategoryUpdateDTO);
    Task DeleteByIdAsync(Guid id);
}
