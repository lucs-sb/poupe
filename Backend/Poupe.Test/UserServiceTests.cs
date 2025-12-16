using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Poupe.Application.Resources;
using Poupe.Application.Services;
using Poupe.Domain.DTOs.User;
using Poupe.Domain.Entities;
using Poupe.Domain.Interfaces.Repositories;
using Poupe.Domain.Interfaces.Repositories.Base;

namespace Poupe.Test;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<ITransactionRepository> _transactionRepository;
    private Mock<IUserRepository> _userRepository;
    private Mock<IPasswordHasher<User>> _passwordHasher;
    private User _user;
    private UserCreateDTO _userCreateDTO;
    private UserUpdateDTO _userUpdateDTO;
    private UserService _service;

    [SetUp]
    public void SetUp()
    {
        _user = new() { Id = Guid.NewGuid(), Name = "Lucas", Age = 23 };

        _userCreateDTO = new UserCreateDTO("Lucas", 23, "lucas@lucas.com", "1234");

        _userUpdateDTO = new UserUpdateDTO("Lucas", 24, "lucas@lucas.com");

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _userRepository = new Mock<IUserRepository>();

        _transactionRepository = new Mock<ITransactionRepository>();

        _passwordHasher = new Mock<IPasswordHasher<User>>();

        _service = new UserService(_unitOfWorkMock.Object, _userRepository.Object, _transactionRepository.Object, _passwordHasher.Object);
    }

    [Test]
    public async Task CreateAsync_WhenUserExists_ShouldThrowInvalidOperationExceptionException()
    {
        // Arrange
        _userRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(_user);

        // Act
        Func<Task> act = async () => await _service.CreateAsync(_userCreateDTO);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(BusinessMessage.AlreadyExists_Warning);
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
        _unitOfWorkMock.Setup(r => r.Repository<User>().AddAsync(_user)).Returns(Task.CompletedTask);

        // Act
        UserResponseDTO result = await _service.CreateAsync(_userCreateDTO);

        // Assert
        UserResponseDTO expected = new(Guid.Empty, "Lucas", 23, "lucas@lucas.com", 0, 0, 0);
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetByIdAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((UserResponseDTO)null!);

        // Act
        Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

    [Test]
    public async Task UpdateAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.UpdateAsync(Guid.NewGuid(), _userUpdateDTO);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

    [Test]
    public async Task UpdateAsync_WhenFound_ShouldUpdateAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);

        // Act
        await _service.UpdateAsync(_user.Id!.Value, _userUpdateDTO);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<User>().Update(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsync_WhenUserNotFound_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null!);

        // Act
        Func<Task> act = async () => await _service.DeleteByIdAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(string.Format(BusinessMessage.NotFound_Warning, "Usuário"));
    }

    [Test]
    public async Task DeleteByIdAsync_WhenFound_ShouldAddAndCommit()
    {
        // Arrange
        _unitOfWorkMock.Setup(r => r.Repository<User>().GetByIdAsync(_user.Id!.Value)).ReturnsAsync(_user);
        _unitOfWorkMock.Setup(r => r.Repository<User>().Remove(_user));

        // Act
        await _service.DeleteByIdAsync(_user.Id!.Value);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Repository<User>().Remove(It.IsAny<User>()), Times.Once);
        _transactionRepository.Verify(r => r.DeleteByUserId(It.IsAny<Guid>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }
}
