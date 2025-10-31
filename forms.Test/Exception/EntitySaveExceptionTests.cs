using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class EntitySaveExceptionTests
{
    [Fact]
    public void Constructor_Default_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntitySaveException();

        // Assert
        exception.Message.Should().Be("Entity save failed");
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntitySaveException("Save failed");

        // Assert
        exception.Message.Should().Be("Save failed");
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Arrange
        var innerException = new System.Exception("Database error");

        // Act
        var exception = new EntitySaveException("Save failed", innerException);

        // Assert
        exception.Message.Should().Be("Save failed");
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
