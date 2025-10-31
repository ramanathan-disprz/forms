using FluentAssertions;
using forms.Exception;
using System.Net;
using Xunit;

namespace forms.Test.Exception;

public class DisprzExceptionTests
{
    [Fact]
    public void Constructor_WithMessage_ShouldSetDefaultStatusCode()
    {
        // Act
        var exception = new DisprzException("Test message", HttpStatusCode.BadRequest);

        // Assert
        exception.Message.Should().Be("Test message");
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Constructor_WithMessageAndStatusCode_ShouldSetCorrectProperties()
    {
        // Act
        var exception = new DisprzException("Test message", HttpStatusCode.BadRequest);

        // Assert
        exception.Message.Should().Be("Test message");
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerException_ShouldSetCorrectProperties()
    {
        // Act
        var exception = new DisprzException("Test message", HttpStatusCode.BadRequest);

        // Assert
        exception.Message.Should().Be("Test message");
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}
