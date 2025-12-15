using FluentAssertions;
using Moq;
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
    private TransactionCreateDTO _transactionCreateDTO;
    private TransactionUpdateDTO _transactionUpdateDTO;
    private TransactionService _service;
    Guid _id;
    Guid _categoryId;
    Guid _userId;

    [SetUp]
    public void SetUp()
    {
        _id = Guid.NewGuid();
        _categoryId = Guid.NewGuid();
        _userId = Guid.NewGuid();

        _transaction = new() { Id = _id, Description = "pão", Value = 3, Type = TransactionType.Expense, CategoryId = _categoryId, UserId = _userId };

        _transactionCreateDTO = new TransactionCreateDTO("pão", 3, TransactionType.Expense, _categoryId, _userId);

        _transactionUpdateDTO = new TransactionUpdateDTO("pão", 4, TransactionType.Expense, _categoryId, _userId);

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new TransactionService(_unitOfWorkMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWorkMock = default!;
        _transactionCreateDTO = default!;
        _service = default!;
    }

    [Test]
    public async Task CreateAsync_WhenNewTransaction_ShouldAddAndCommit()
    {
        // Arrange
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
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().AddAsync(_transaction)).Returns(Task.CompletedTask);

        // Act
        TransactionResponseDTO result = await _service.CreateAsync(_transactionCreateDTO);

        // Assert
        TransactionResponseDTO expected = new(null!, "pão", 3, TransactionType.Expense, _categoryId.ToString(), _userId.ToString());
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetByIdAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task GetByIdAsync_WhenFound_ShouldReturnTransactionResponseDTO()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_id)).ReturnsAsync(_transaction);

        // Act
        TransactionResponseDTO result = await _service.GetByIdAsync(_id);

        // Assert
        TransactionResponseDTO expected = new(_id.ToString(), "pão", 3, TransactionType.Expense, _categoryId.ToString(), _userId.ToString());
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void UpdateAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.UpdateAsync(Guid.NewGuid(), _transactionUpdateDTO);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task UpdateAsync_WhenFound_ShouldUpdateAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_id)).ReturnsAsync(_transaction);

        // Act
        await _service.UpdateAsync(_id, _transactionUpdateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Transaction>().Update(It.IsAny<Transaction>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public void DeleteByIdAsync_WhenTransactionNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Transaction)null!);

        // Act
        Func<Task> act = async () => await _service.DeleteByIdAsync(Guid.NewGuid());

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task DeleteByIdAsync_WhenFound_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().GetByIdAsync(_id)).ReturnsAsync(_transaction);
        _unitOfWorkMock.Setup(r => r.Repository<Transaction>().Remove(_transaction));

        // Act
        await _service.DeleteByIdAsync(_id);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<Transaction>().Remove(It.IsAny<Transaction>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}
