using AutoMapper;
using FluentAssertions;
using forms.Model;
using forms.Repository.Interfaces;
using forms.Request;
using forms.Service.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Test.Services;

public class UserServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<UserService>>();
        _repositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_mapperMock.Object, _loggerMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void Index_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "User 1", Email = "user1@test.com", Password = "123456" },
            new User { Id = 2, Name = "User 2", Email = "user2@test.com", Password = "123456" }
        };

        _repositoryMock.Setup(x => x.FindAll()).Returns(users);

        // Act
        var result = _userService.Index();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.FindAll(), Times.Once);
    }

    [Fact]
    public void Fetch_WithValidId_ShouldReturnUser()
    {
        // Arrange
        var userId = 1L;
        var user = new User { Id = userId, Name = "Test User", Email = "test@test.com", Password = "123456" };

        _repositoryMock.Setup(x => x.FindOrThrow(userId)).Returns(user);

        // Act
        var result = _userService.Fetch(userId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);
        _repositoryMock.Verify(x => x.FindOrThrow(userId), Times.Once);
    }

    [Fact]
    public void Update_WithValidRequest_ShouldUpdateUser()
    {
        // Arrange
        var userId = 1L;
        var existingUser = new User { Id = userId, Name = "Old Name", Email = "old@test.com", Password = "123456" };
        var request = new UserRequest { Name = "New Name", Email = "new@test.com" };
        var updatedUser = new User { Id = userId, Name = "New Name", Email = "new@test.com", Password = "123456" };

        _repositoryMock.Setup(x => x.FindOrThrow(userId)).Returns(existingUser);
        _mapperMock.Setup(x => x.Map(request, existingUser)).Returns(updatedUser);
        _repositoryMock.Setup(x => x.Update(It.IsAny<User>())).Returns(updatedUser);

        // Act
        var result = _userService.Update(userId, request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("New Name");
        _repositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public void Delete_WithValidId_ShouldDeleteUser()
    {
        // Arrange
        var userId = 1L;
        var user = new User { Id = userId, Name = "Test User" , Password = "123456", Email = "test@test.com"};

        _repositoryMock.Setup(x => x.FindOrThrow(userId)).Returns(user);

        // Act
        _userService.Delete(userId);

        // Assert
        _repositoryMock.Verify(x => x.FindOrThrow(userId), Times.Once);
        _repositoryMock.Verify(x => x.Delete(user), Times.Once);
    }
}
