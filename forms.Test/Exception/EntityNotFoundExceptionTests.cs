using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class EntityNotFoundExceptionTests
{
    [Fact]
    public void Constructor_Default_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntityNotFoundException();

        // Assert
        exception.Message.Should().Be("Entity not found");
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntityNotFoundException("Custom message");

        // Assert
        exception.Message.Should().Be("Custom message");
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void Constructor_WithId_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntityNotFoundException(123);

        // Assert
        exception.Message.Should().Be("Entity with id 123 not found");
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Arrange
        var innerException = new System.Exception("Inner exception");

        // Act
        var exception = new EntityNotFoundException("Custom message", innerException);

        // Assert
        exception.Message.Should().Be("Custom message");
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
