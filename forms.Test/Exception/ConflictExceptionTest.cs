using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class ConflictExceptionTests
{
    [Fact]
    public void Constructor_Default_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new ConflictException();

        // Assert
        exception.Message.Should().Be("Conflict");
        exception.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new ConflictException("Resource already exists");

        // Assert
        exception.Message.Should().Be("Resource already exists");
        exception.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Arrange
        var innerException = new System.Exception("Database conflict");

        // Act
        var exception = new ConflictException("Resource conflict", innerException);

        // Assert
        exception.Message.Should().Be("Resource conflict");
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}
