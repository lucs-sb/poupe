using Mapster;
using Poupe.Application.Resources;
using Poupe.Domain.DTOs.Category;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Cria uma nova categoria no sistema.
    /// </summary>
    /// <param name="categoryCreateDTO">Dados da categoria.</param>
    public async Task<CategoryResponseDTO> CreateAsync(CategoryCreateDTO categoryCreateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Category category = categoryCreateDTO.Adapt<Category>();

            await _unitOfWork.Repository<Category>().AddAsync(category);

            await _unitOfWork.CommitAsync();

            return category.Adapt<CategoryResponseDTO>();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// Deleta uma categoria pelo id.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a categoria não é encontrada.
    /// </exception>
    public async Task DeleteByIdAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

            _unitOfWork.Repository<Category>().Remove(category);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }

    /// <summary>
    /// Busca por todas as categorias.
    /// </summary>
    public async Task<List<CategoryResponseDTO>> GetAllAsync()
    {
        List<Category> categorys = await _unitOfWork.Repository<Category>().GetAllAsync();

        return categorys.Adapt<List<CategoryResponseDTO>>();
    }

    /// <summary>
    /// Busca uma categoria pelo id.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a categoria não é encontrada.
    /// </exception>
    public async Task<CategoryResponseDTO> GetByIdAsync(Guid id)
    {
        Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

        return category.Adapt<CategoryResponseDTO>();
    }

    /// <summary>
    /// Edita uma categoria pelo id.
    /// </summary>
    /// <param name="id">Identificador da categoria.</param>
    /// <param name="categoryUpdateDTO">Dados da categoria.</param>
    /// <exception cref="KeyNotFoundException">
    /// Lançada quando a categoria não é encontrada.
    /// </exception>
    public async Task UpdateAsync(Guid id, CategoryUpdateDTO categoryUpdateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(id) ?? throw new KeyNotFoundException(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));

            categoryUpdateDTO.Adapt(category);

            _unitOfWork.Repository<Category>().Update(category);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();

            throw;
        }
    }
}
