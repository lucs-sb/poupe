using FluentAssertions;
using Moq;
using Poupe.Application.Services;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Test;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private UserCreateDTO _userCreateDTO;
    private UserUpdateDTO _userUpdateDTO;
    private UserService _service;

    [SetUp]
    public void SetUp()
    {
        _userCreateDTO = new UserCreateDTO("Lucas", 23);

        _userUpdateDTO = new UserUpdateDTO("Lucas", 24);

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new UserService(_unitOfWorkMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _unitOfWorkMock = default!;
        _userCreateDTO = default!;
        _service = default!;
    }

    [Test]
    public async Task CreateAsync_WhenNewUser_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

        // Act
        UserResponseDTO result = await _service.CreateAsync(_userCreateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<User>().AddAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task CreateAsync_WhenNewUser_ShouldReturnUserResponseDTO()
    {
        // Arrange
        User user = new() { Name = "Lucas", Age = 23 };
        _unitOfWorkMock.Setup(r => r.Repository<User>().AddAsync(user)).Returns(Task.CompletedTask);

        // Act
        UserResponseDTO result = await _service.CreateAsync(_userCreateDTO);

        // Assert
        UserResponseDTO expected = new(null!, "Lucas", 23);
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetByIdAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task GetByIdAsync_WhenFound_ShouldReturnUserResponseDTO()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        User user = new() { Id = id, Name = "Lucas", Age = 23 };
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        UserResponseDTO result = await _service.GetByIdAsync(id);

        // Assert
        UserResponseDTO expected = new(id.ToString(), "Lucas", 23);
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void UpdateAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.UpdateAsync(Guid.NewGuid(), _userUpdateDTO);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task UpdateAsync_WhenFound_ShouldUpdateAndCommit()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        User user = new() { Id = id, Name = "Lucas", Age = 23 };
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(id)).ReturnsAsync(user);

        // Act
        await _service.UpdateAsync(id, _userUpdateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<User>().Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public void DeleteByIdAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.DeleteByIdAsync(Guid.NewGuid());

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Usuário não encontrado");
    }

    [Test]
    public async Task DeleteByIdAsync_WhenFound_ShouldAddAndCommit()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        User user = new() { Id = id, Name = "Lucas", Age = 23 };
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(id)).ReturnsAsync(user);
        _unitOfWorkMock.Setup(r => r.Repository<User>().Remove(user));

        // Act
        await _service.DeleteByIdAsync(id);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<User>().Remove(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}
