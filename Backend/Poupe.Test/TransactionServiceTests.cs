using FluentAssertions;
using Moq;
using Poupe.Application.Resources;
using Poupe.Application.Services;
using Poupe.Domain.DTOs.Transaction;
using Poupe.Domain.Entities;
using Poupe.Domain.Enums;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Test;

[TestFixture]
public class TransactionServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Transaction _transaction;
    private User _user;
    private Category _category;
    private TransactionCreateDTO _transactionCreateDTO;
    private TransactionUpdateDTO _transactionUpdateDTO;
    private TransactionService _service;

    [SetUp]
    public void SetUp()
    {
        Guid categoryId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();

        _transaction = new() { Id = Guid.NewGuid(), Description = "pão", Value = 3, Type = TransactionType.Expense, CategoryId = categoryId, UserId = userId };

        _user = new() { Id = userId, Name = "Lucas", Age = 23 };

        _category = new() { Id = categoryId, Description = "Casa", Purpose = CategoryType.Expense };

        _transactionCreateDTO = new TransactionCreateDTO("pão", 3, TransactionType.Expense, categoryId, userId);

        _transactionUpdateDTO = new TransactionUpdateDTO("pão", 4, TransactionType.Expense, categoryId);

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new TransactionService(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task CreateAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

    [TestCase(CategoryType.Income)]
    [TestCase(CategoryType.Both)]
    public async Task CreateAsync_WhenUserIsNotAdultAndIsIncome_ShouldThrowInvalidOperationException(CategoryType categoryType)
    {
        // Arrange
        _user.Age = 16; 
        _category.Purpose = categoryType;
        _transactionCreateDTO = new TransactionCreateDTO("pão", 3, TransactionType.Income, _category.Id!.Value, _user.Id!.Value);
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(BusinessMessage.LegalAge_Error);
    }

    [Test]
    public async Task CreateAsync_WhenCategoryTypeIsNotEqualToTransactionType_ShouldThrowInvalidOperationException()
    {
        // Arrange=
        _category.Purpose = CategoryType.Income;
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(BusinessMessage.TransactionType_Error);
    }

    [Test]
    public async Task CreateAsync_WhenCategoryNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Category)null!);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Categoria"));
    }

    [Test]
    public async Task CreateAsync_WhenNewTransaction_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);

        // Act
        TransactionResponseDTO result = await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Transaction>().AddAsync(It.IsAny<Transaction>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WhenNewTransaction_ShouldReturnTransactionResponseDTO()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().AddAsync(_transaction)).Returns(Task.CompletedTask);

        // Act
        TransactionResponseDTO result = await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        TransactionResponseDTO expected = new(null!, "pão", 3, TransactionType.Expense, _category.Id!.Value.ToString(), _user.Id!.Value.ToString());
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetByIdAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(It.IsAny<Guid>());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Transação"));
    }

    [Test]
    public async Task GetByIdAsync_WhenFound_ShouldReturnTransactionResponseDTO()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_transaction.Id!.Value)).ReturnsAsync(_transaction);

        // Act
        TransactionResponseDTO result = await _service.GetByIdAsync(_transaction.Id!.Value);

        // Assert
        TransactionResponseDTO expected = new(_transaction.Id!.Value.ToString(), "pão", 3, TransactionType.Expense, _category.Id!.Value.ToString(), _user.Id!.Value.ToString());
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task UpdateAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.UpdateAsync(_transaction.Id!.Value, _transactionUpdateDTO);

        // Assert
       await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Transação"));
    }

    [Test]
    public async Task UpdateAsync_WhenFound_ShouldUpdateAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Category>().GetByIdAsync(_category.Id!.Value)).ReturnsAsync(_category);
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_transaction.Id!.Value)).ReturnsAsync(_transaction);

        // Act
        await _service.UpdateAsync(_transaction.Id!.Value, _transactionUpdateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Transaction>().Update(It.IsAny<Transaction>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.DeleteByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Transação"));
    }

    [Test]
    public async Task DeleteByIdAsync_WhenFound_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_transaction.Id!.Value)).ReturnsAsync(_transaction);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().Remove(_transaction));

        // Act
        await _service.DeleteByIdAsync(_transaction.Id!.Value);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Transaction>().Remove(It.IsAny<Transaction>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}
