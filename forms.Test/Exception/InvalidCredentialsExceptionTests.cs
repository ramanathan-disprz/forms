using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class InvalidCredentialsExceptionTests
{
    [Fact]
    public void Constructor_Default_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new InvalidCredentialsException();

        // Assert
        exception.Message.Should().Be("Invalid credentials");
        exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new InvalidCredentialsException("Wrong username or password");

        // Assert
        exception.Message.Should().Be("Wrong username or password");
        exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Arrange
        var innerException = new System.Exception("Authentication failed");

        // Act
        var exception = new InvalidCredentialsException("Invalid login", innerException);

        // Assert
        exception.Message.Should().Be("Invalid login");
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
