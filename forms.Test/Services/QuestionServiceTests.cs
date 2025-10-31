using FluentAssertions;
using forms.Mapping;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request.FormAuthoring;
using forms.Service.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Tests.Services;

public class QuestionServiceTests
{
    private readonly Mock<IQuestionMapper> _questionMapperMock;
    private readonly Mock<ILogger<QuestionService>> _loggerMock;
    private readonly Mock<IQuestionRepository> _repositoryMock;
    private readonly QuestionService _questionService;

    public QuestionServiceTests()
    {
        _questionMapperMock = new Mock<IQuestionMapper>();
        _loggerMock = new Mock<ILogger<QuestionService>>();
        _repositoryMock = new Mock<IQuestionRepository>();
        _questionService = new QuestionService(
            _questionMapperMock.Object,
            _loggerMock.Object,
            _repositoryMock.Object
        );
    }

    [Fact]
    public void IndexByFormId_ShouldReturnQuestions()
    {
        // Arrange
        var formId = "form123";
        var questions = new List<Question>
        {
            new ShortTextQuestion { Id = "q1", FormId = formId, QuestionText = "Question 1" },
            new ShortTextQuestion { Id = "q2", FormId = formId, QuestionText = "Question 2" }
        };

        _repositoryMock.Setup(x => x.IndexByFormId(formId)).Returns(questions);

        // Act
        var result = _questionService.IndexByFormId(formId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.IndexByFormId(formId), Times.Once);
    }

    [Fact]
    public void Fetch_WithValidId_ShouldReturnQuestion()
    {
        // Arrange
        var questionId = "q123";
        var question = new ShortTextQuestion { Id = questionId, QuestionText = "Test Question" };

        _repositoryMock.Setup(x => x.FindOrThrow(questionId)).Returns(question);

        // Act
        var result = _questionService.Fetch(questionId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(questionId);
        _repositoryMock.Verify(x => x.FindOrThrow(questionId), Times.Once);
    }

    [Fact]
    public void Create_WithValidRequest_ShouldCreateQuestion()
    {
        // Arrange
        var request = new QuestionRequest { FormId = "form123", QuestionText = "New Question" };
        var question = new ShortTextQuestion { Id = "q1", FormId = "form123", QuestionText = "New Question" };

        _questionMapperMock.Setup(x => x.Map(request)).Returns(question);
        _repositoryMock.Setup(x => x.Create(question)).Returns(question);

        // Act
        var result = _questionService.Create(request);

        // Assert
        result.Should().NotBeNull();
        result.QuestionText.Should().Be("New Question");
        _repositoryMock.Verify(x => x.Create(question), Times.Once);
    }

    [Fact]
    public void CreateMany_WithValidRequests_ShouldCreateQuestions()
    {
        // Arrange
        var requests = new List<QuestionRequest>
        {
            new QuestionRequest { FormId = "form123", QuestionText = "Question 1" },
            new QuestionRequest { FormId = "form123", QuestionText = "Question 2" }
        };

        var questions = new List<Question>
        {
            new ShortTextQuestion { Id = "q1", FormId = "form123", QuestionText = "Question 1" },
            new ShortTextQuestion { Id = "q2", FormId = "form123", QuestionText = "Question 2" }
        };

        _questionMapperMock.Setup(x => x.Map(requests)).Returns(questions);
        _repositoryMock.Setup(x => x.CreateMany(questions)).Returns(questions);

        // Act
        var result = _questionService.CreateMany(requests);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.CreateMany(questions), Times.Once);
    }

    [Fact]
    public void Update_WithValidRequest_ShouldUpdateQuestion()
    {
        // Arrange
        var questionId = "q123";
        var request = new QuestionRequest { FormId = "form123", QuestionText = "Updated Question" };
        var existingQuestion = new ShortTextQuestion { Id = questionId, FormId = "form123", QuestionText = "Old Question" };
        var updatedQuestion = new ShortTextQuestion { Id = questionId, FormId = "form123", QuestionText = "Updated Question" };

        _repositoryMock.Setup(x => x.FindOrThrow(questionId)).Returns(existingQuestion);
        _questionMapperMock.Setup(x => x.Merge(existingQuestion, request)).Returns(updatedQuestion);
        _repositoryMock.Setup(x => x.Update(questionId, updatedQuestion)).Returns(updatedQuestion);

        // Act
        var result = _questionService.Update(questionId, request);

        // Assert
        result.Should().NotBeNull();
        result.QuestionText.Should().Be("Updated Question");
        _repositoryMock.Verify(x => x.Update(questionId, updatedQuestion), Times.Once);
    }

    [Fact]
    public void UpdateMany_WithValidRequests_ShouldUpdateQuestions()
    {
        // Arrange
        var requests = new List<QuestionRequest>
        {
            new QuestionRequest { Id = "q1", FormId = "form123", QuestionText = "Updated Q1" },
            new QuestionRequest { Id = "q2", FormId = "form123", QuestionText = "Updated Q2" }
        };

        var updatedQuestions = new List<Question>
        {
            new ShortTextQuestion { Id = "q1", FormId = "form123", QuestionText = "Updated Q1" },
            new ShortTextQuestion { Id = "q2", FormId = "form123", QuestionText = "Updated Q2" }
        };

        _questionMapperMock.Setup(x => x.Merge(requests)).Returns(updatedQuestions);
        _repositoryMock.Setup(x => x.UpdateMany(updatedQuestions, It.IsAny<Func<Question, string>>()))
            .Returns(updatedQuestions);

        // Act
        var result = _questionService.UpdateMany(requests);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.UpdateMany(updatedQuestions, It.IsAny<Func<Question, string>>()), Times.Once);
    }

    [Fact]
    public void Delete_WithValidId_ShouldDeleteQuestion()
    {
        // Arrange
        var questionId = "q123";

        // Act
        _questionService.Delete(questionId);

        // Assert
        _repositoryMock.Verify(x => x.Delete(questionId), Times.Once);
    }

    [Fact]
    public void DeleteByFormId_ShouldDeleteAllFormQuestions()
    {
        // Arrange
        var formId = "form123";

        // Act
        _questionService.DeleteByFormId(formId);

        // Assert
        _repositoryMock.Verify(x => x.DeleteByFormId(formId), Times.Once);
    }
}
