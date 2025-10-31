using AutoMapper;
using FluentAssertions;
using forms.Dto;
using forms.GraphQL.Mutations;
using forms.Model;
using forms.Request;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Mutations;

public class UserMutationTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserService> _serviceMock;
    private readonly UserMutation _mutation;

    public UserMutationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IUserService>();
        _mutation = new UserMutation(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public void UpdateUser_ShouldReturnUpdatedUserDto()
    {
        // Arrange
        var request = new UserRequest { Name = "Updated", Email = "updated@test.com" };
        var user = new User { Id = 1, Name = "Updated", Email = "updated@test.com", Password = "password" };
        var userDto = new UserDto { Id = 1, Name = "Updated", Email = "updated@test.com" };

        _serviceMock.Setup(x => x.Update(1, request)).Returns(user);
        _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _mutation.UpdateUser(1, request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Updated");
        _serviceMock.Verify(x => x.Update(1, request), Times.Once);
    }

    [Fact]
    public void DeleteUser_ShouldReturnTrue()
    {
        // Arrange
        _serviceMock.Setup(x => x.Delete(1));

        // Act
        var result = _mutation.DeleteUser(1);

        // Assert
        result.Should().BeTrue();
        _serviceMock.Verify(x => x.Delete(1), Times.Once);
    }
}
