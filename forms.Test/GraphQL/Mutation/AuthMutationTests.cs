using AutoMapper;
using FluentAssertions;
using forms.Dto;
using forms.GraphQL.Mutations;
using forms.Model;
using forms.Request;
using forms.Service.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Mutations;

public class AuthMutationTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAuthService> _serviceMock;
    private readonly Mock<ILogger<AuthMutation>> _loggerMock;
    private readonly AuthMutation _mutation;

    public AuthMutationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IAuthService>();
        _loggerMock = new Mock<ILogger<AuthMutation>>();
        _mutation = new AuthMutation(_mapperMock.Object, _serviceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void RegisterUser_ShouldReturnUserDto()
    {
        // Arrange
        var request = new UserRequest { Name = "Test", Email = "test@test.com", Password = "password" };
        var user = new User { Id = 1, Name = "Test", Email = "test@test.com", Password="password" };
        var userDto = new UserDto { Id = 1, Name = "Test", Email = "test@test.com" };

        _serviceMock.Setup(x => x.Register(request)).Returns(user);
        _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _mutation.RegisterUser(request);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        _serviceMock.Verify(x => x.Register(request), Times.Once);
    }
}
