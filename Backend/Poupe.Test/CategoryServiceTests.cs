using FluentAssertions;
using Moq;
using Poupe.Application.Resources;
using Poupe.Application.Services;
using Poupe.Domain.DTOs.Category;
using Poupe.Domain.Entities;
using Poupe.Domain.Enums;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Test;

[TestFixture]
public class CategoryServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Category _category;
    private CategoryCreateDTO _categoryCreateDTO;
    private CategoryUpdateDTO _categoryUpdateDTO;
    private CategoryService _service;

    [SetUp]
    public void SetUp()
    {
        _category = new() { Id = Guid.NewGuid(), Description = "Casa", Purpose = CategoryType.Both };

        _categoryCreateDTO = new CategoryCreateDTO("Casa", CategoryType.Both);

        _categoryUpdateDTO = new CategoryUpdateDTO("Casa", CategoryType.Both);

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new CategoryService(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task CreateAsync_WhenNewCategory_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().AddAsync(It.IsAny<Category>())).Returns(Task.CompletedTask);

        // Act
        CategoryResponseDTO result = await _service.CreateAsync(_categoryCreateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Category>().AddAsync(It.IsAny<Category>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WhenNewCategory_ShouldReturnCategoryResponseDTO()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().AddAsync(_category)).Returns(Task.CompletedTask);

        // Act
        CategoryResponseDTO result = await _service.CreateAsync(_categoryCreateDTO);

        // Assert
        CategoryResponseDTO expected = new(null!, "Casa", CategoryType.Both);
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetByIdAsync_WhenCategoryNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category)null!);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));
    }

    [Test]
    public async Task GetByIdAsync_WhenFound_ShouldReturnCategoryResponseDTO()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);

        // Act
        CategoryResponseDTO result = await _service.GetByIdAsync(_category.Id!.Value);

        // Assert
        CategoryResponseDTO expected = new(_category.Id!.Value.ToString(), "Casa", CategoryType.Both);
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task UpdateAsync_WhenCategoryNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category)null!);

        // Act
        Func<Task> act = async () => await _service.UpdateAsync(Guid.NewGuid(), _categoryUpdateDTO);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));
    }

    [Test]
    public async Task UpdateAsync_WhenFound_ShouldUpdateAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);

        // Act
        await _service.UpdateAsync(_category.Id!.Value, _categoryUpdateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Category>().Update(It.IsAny<Category>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsync_WhenCategoryNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category)null!);

        // Act
        Func<Task> act = async () => await _service.DeleteByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));
    }

    [Test]
    public async Task DeleteByIdAsync_WhenFound_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<Category>().Remove(_category));

        // Act
        await _service.DeleteByIdAsync(_category.Id!.Value);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Category>().Remove(It.IsAny<Category>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}
