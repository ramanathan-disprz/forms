using AutoMapper;
using FluentAssertions;
using forms.GraphQL.Mutations;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Mutations;

public class QuestionMutationTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IQuestionService> _serviceMock;
    private readonly QuestionMutation _mutation;

    public QuestionMutationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IQuestionService>();
        _mutation = new QuestionMutation(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public void DeleteQuestion_ShouldReturnTrue()
    {
        // Arrange
        _serviceMock.Setup(x => x.Delete("1"));

        // Act
        var result = _mutation.DeleteQuestion("1");

        // Assert
        result.Should().BeTrue();
        _serviceMock.Verify(x => x.Delete("1"), Times.Once);
    }
}
