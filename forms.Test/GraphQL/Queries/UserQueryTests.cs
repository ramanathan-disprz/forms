using AutoMapper;
using FluentAssertions;
using forms.Dto;
using forms.GraphQL.Queries;
using forms.Model;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Tests.GraphQL.Queries;

public class UserQueryTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IUserService> _serviceMock;
    private readonly UserQuery _userQuery;

    public UserQueryTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IUserService>();
        _userQuery = new UserQuery(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public void Hello_ShouldReturnGreeting()
    {
        // Act
        var result = _userQuery.Hello();

        // Assert
        result.Should().Be("Hello Ram!");
    }

    [Fact]
    public void IndexUser_ShouldReturnAllUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com",
                Password = "12345r23ewtfgeqwasdgwervgr"
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Password = "12345r23ewtfgeqwasdgwervgr"

            }
        };

        var userDtos = new List<UserDto>
        {
            new UserDto
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@example.com"
            },
            new UserDto
            {
                Id = 2,
                Name = "Jane Smith",
                Email = "jane@example.com"
            }
        };

        _serviceMock.Setup(x => x.Index()).Returns(users);
        _mapperMock.Setup(x => x.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);

        // Act
        var result = _userQuery.IndexUser();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("John Doe");
        result.Last().Name.Should().Be("Jane Smith");

        _serviceMock.Verify(x => x.Index(), Times.Once);
        _mapperMock.Verify(x => x.Map<IEnumerable<UserDto>>(users), Times.Once);
    }

    [Fact]
    public void IndexUser_WithNoUsers_ShouldReturnEmptyList()
    {
        // Arrange
        var emptyUsers = new List<User>();
        var emptyUserDtos = new List<UserDto>();

        _serviceMock.Setup(x => x.Index()).Returns(emptyUsers);
        _mapperMock.Setup(x => x.Map<IEnumerable<UserDto>>(emptyUsers)).Returns(emptyUserDtos);

        // Act
        var result = _userQuery.IndexUser();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();

        _serviceMock.Verify(x => x.Index(), Times.Once);
    }

    [Fact]
    public void FetchUser_WithValidId_ShouldReturnUser()
    {
        // Arrange
        var userId = 1L;
        var user = new User
        {
            Id = userId,
            Name = "John Doe",
            Email = "john@example.com",
            Password = "12345r23ewtfgeqwasdgwervgr"

        };

        var userDto = new UserDto
        {
            Id = userId,
            Name = "John Doe",
            Email = "john@example.com"
        };

        _serviceMock.Setup(x => x.Fetch(userId)).Returns(user);
        _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _userQuery.FetchUser(userId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);
        result.Name.Should().Be("John Doe");
        result.Email.Should().Be("john@example.com");

        _serviceMock.Verify(x => x.Fetch(userId), Times.Once);
        _mapperMock.Verify(x => x.Map<UserDto>(user), Times.Once);
    }

    [Fact]
    public void FetchUser_WithDifferentId_ShouldCallServiceWithCorrectId()
    {
        // Arrange
        var userId = 999L;
        var user = new User
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com",
            Password = "12345r23ewtfgeqwasdgwervgr"

        };

        var userDto = new UserDto
        {
            Id = userId,
            Name = "Test User",
            Email = "test@example.com"
        };

        _serviceMock.Setup(x => x.Fetch(userId)).Returns(user);
        _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _userQuery.FetchUser(userId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);

        _serviceMock.Verify(x => x.Fetch(userId), Times.Once);
        _serviceMock.Verify(x => x.Fetch(It.IsAny<long>()), Times.Once);
    }
}
