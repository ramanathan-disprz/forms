using AutoMapper;
using FluentAssertions;
using forms.Dto;
using forms.Exception;
using forms.Model;
using forms.Repository.Interfaces;
using forms.Request;
using forms.Service.Implementation;
using forms.Utils.Security;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Test.Services;

public class AuthServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<AuthService>>();
        _repositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _authService = new AuthService(
            _mapperMock.Object,
            _loggerMock.Object,
            _repositoryMock.Object,
            _passwordHasherMock.Object
        );
    }

    [Fact]
    public void Register_WithValidRequest_ShouldCreateUser()
    {
        // Arrange
        var userRequest = new UserRequest
        {
            Name = "John Doe",
            Email = "test@example.com",
            Password = "password123"
        };

        var hashedPassword = "hashed_password";
        var mappedUser = new User
        {
            Email = userRequest.Email,
            Password = hashedPassword,
            Name = userRequest.Name
        };

        _repositoryMock.Setup(x => x.ExistsByEmail(userRequest.Email))
            .Returns(false);

        _passwordHasherMock.Setup(x => x.HashPassword(userRequest.Password))
            .Returns(hashedPassword);

        _mapperMock.Setup(x => x.Map<User>(It.IsAny<UserRequest>()))
            .Returns(mappedUser);

        _repositoryMock.Setup(x => x.Create(It.IsAny<User>()))
            .Returns(mappedUser);

        // Act
        var result = _authService.Register(userRequest);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(userRequest.Email);
    }

    [Fact]
    public void Register_WithExistingEmail_ShouldThrowConflictException()
    {
        // Arrange
        var userRequest = new UserRequest
        {
            Name = "John Doe",
            Email = "existing@example.com",
            Password = "password123"
        };

        _repositoryMock.Setup(x => x.ExistsByEmail(userRequest.Email))
            .Returns(true);

        // Act & Assert
        var exception = Assert.Throws<ConflictException>(() => _authService.Register(userRequest));
        exception.Message.Should().Contain(userRequest.Email);
    }

    [Fact]
    public void Register_WithNullPassword_ShouldThrowBadRequestException()
    {
        // Arrange
        var userRequest = new UserRequest
        {
            Name = "John Doe",
            Email = "test@example.com",
            Password = null
        };

        _repositoryMock.Setup(x => x.ExistsByEmail(userRequest.Email))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<BadRequestException>(() => _authService.Register(userRequest));
        exception.Message.Should().Be("Password is required");
    }

    [Fact]
    public void Login_WithValidCredentials_ShouldReturnAuthResponse()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = "password123"
        };

        var existingUser = new User
        {
            Id = 1,
            Name = "John Doe",
            Email = loginRequest.Email,
            Password = "hashed_password"
        };

        _repositoryMock.Setup(x => x.FindByEmailOrThrow(loginRequest.Email))
            .Returns(existingUser);

        _passwordHasherMock.Setup(x => x.VerifyPassword(loginRequest.Password, existingUser.Password))
            .Returns(true);

        // Act
        var result = _authService.Login(loginRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AuthResponseDto>();
    }

    [Fact]
    public void Login_WithInvalidPassword_ShouldThrowInvalidCredentialsException()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = "wrong_password"
        };

        var existingUser = new User
        {
            Id = 1,
            Name = "John Doe",
            Email = loginRequest.Email,
            Password = "hashed_password"
        };

        _repositoryMock.Setup(x => x.FindByEmailOrThrow(loginRequest.Email))
            .Returns(existingUser);

        _passwordHasherMock.Setup(x => x.VerifyPassword(loginRequest.Password, existingUser.Password))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<InvalidCredentialsException>(() => _authService.Login(loginRequest));
        exception.Message.Should().Be("Invalid credentials");
    }
}
