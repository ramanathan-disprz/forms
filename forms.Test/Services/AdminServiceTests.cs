using FluentAssertions;
using forms.Model;
using forms.Repository.Interfaces;
using forms.Service.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Test.Services;

public class AdminServiceTests
{
    private readonly Mock<ILogger<AdminService>> _loggerMock;
    private readonly Mock<IAdminRepository> _repositoryMock;
    private readonly AdminService _adminService;

    public AdminServiceTests()
    {
        _loggerMock = new Mock<ILogger<AdminService>>();
        _repositoryMock = new Mock<IAdminRepository>();
        _adminService = new AdminService(_loggerMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void GetAll_ShouldReturnAllAdmins()
    {
        // Arrange
        var admins = new List<Admin>
        {
            new Admin { UserId = 1 },
            new Admin { UserId = 2 }
        };

        _repositoryMock.Setup(x => x.FindAll()).Returns(admins);

        // Act
        var result = _adminService.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.FindAll(), Times.Once);
    }

    [Fact]
    public void Fetch_WithValidUserId_ShouldReturnAdmin()
    {
        // Arrange
        var userId = 1L;
        var admin = new Admin { UserId = userId, };

        _repositoryMock.Setup(x => x.FindOrThrow(userId)).Returns(admin);

        // Act
        var result = _adminService.Fetch(userId);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(userId);
        _repositoryMock.Verify(x => x.FindOrThrow(userId), Times.Once);
    }
}