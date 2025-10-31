using FluentAssertions;
using forms.Dto.FormAuthoring;
using forms.GraphQL.Queries;
using forms.Mapping;
using forms.Model.FormAuthoring;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Queries;

public class QuestionQueryTests
{
    private readonly Mock<IQuestionService> _serviceMock;
    private readonly Mock<IQuestionMapper> _mapperMock;
    private readonly QuestionQuery _questionQuery;

    public QuestionQueryTests()
    {
        _serviceMock = new Mock<IQuestionService>();
        _mapperMock = new Mock<IQuestionMapper>();
        _questionQuery = new QuestionQuery(_serviceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void IndexQuestionsByFormId_ShouldReturnQuestions()
    {
        // Arrange
        var questions = new List<Question> { new ShortTextQuestion { Id = "1" } };
        var questionDtos = new List<QuestionDto> { new QuestionDto { Id = "1" } };
        
        _serviceMock.Setup(x => x.IndexByFormId("form1")).Returns(questions);
        _mapperMock.Setup(x => x.Map(questions)).Returns(questionDtos);

        // Act
        var result = _questionQuery.IndexQuestionsByFormId("form1");

        // Assert
        result.Should().HaveCount(1);
        _serviceMock.Verify(x => x.IndexByFormId("form1"), Times.Once);
    }

    [Fact]
    public void FetchQuestion_ShouldReturnQuestion()
    {
        // Arrange
        var question = new ShortTextQuestion { Id = "1" };
        var questionDto = new QuestionDto { Id = "1" };
        
        _serviceMock.Setup(x => x.Fetch("1")).Returns(question);
        _mapperMock.Setup(x => x.Map(question)).Returns(questionDto);

        // Act
        var result = _questionQuery.FetchQuestion("1");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("1");
        _serviceMock.Verify(x => x.Fetch("1"), Times.Once);
    }
}
