using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class EntityDeleteExceptionTests
{
    [Fact]
    public void Constructor_Default_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntityDeleteException();

        // Assert
        exception.Message.Should().Be("Entity deletion failed");
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetCorrectMessageAndStatusCode()
    {
        // Act
        var exception = new EntityDeleteException("Delete failed");

        // Assert
        exception.Message.Should().Be("Delete failed");
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Arrange
        var innerException = new System.Exception("Database error");

        // Act
        var exception = new EntityDeleteException("Delete failed", innerException);

        // Assert
        exception.Message.Should().Be("Delete failed");
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}
