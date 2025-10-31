using AutoMapper;
using FluentAssertions;
using forms.Enum;
using forms.Exception;
using forms.Model.FormAuthoring;
using forms.Repository.Interfaces;
using forms.Request.FormAuthoring;
using forms.Service.Implementation;
using forms.Service.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Tests.Services;

public class FormServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<FormService>> _loggerMock;
    private readonly Mock<IFormRepository> _repositoryMock;
    private readonly Mock<IQuestionService> _questionServiceMock;
    private readonly FormService _formService;

    public FormServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<FormService>>();
        _repositoryMock = new Mock<IFormRepository>();
        _questionServiceMock = new Mock<IQuestionService>();
        _formService = new FormService(
            _mapperMock.Object,
            _loggerMock.Object,
            _repositoryMock.Object,
            _questionServiceMock.Object
        );
    }

    [Fact]
    public void Index_ShouldReturnAllForms()
    {
        // Arrange
        var forms = new List<Form>
        {
            new Form { Id = "1", Title = "Form 1" },
            new Form { Id = "2", Title = "Form 2" }
        };

        _repositoryMock.Setup(x => x.FindAll()).Returns(forms);

        // Act
        var result = _formService.Index();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.FindAll(), Times.Once);
    }

    [Fact]
    public void Fetch_WithValidId_ShouldReturnForm()
    {
        // Arrange
        var formId = "form123";
        var form = new Form { Id = formId, Title = "Test Form" };

        _repositoryMock.Setup(x => x.FindOrThrow(formId)).Returns(form);

        // Act
        var result = _formService.Fetch(formId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(formId);
        _repositoryMock.Verify(x => x.FindOrThrow(formId), Times.Once);
    }

    [Fact]
    public void FetchWithQuestions_ShouldReturnFormWithQuestions()
    {
        // Arrange
        var formId = "form123";
        var form = new Form { Id = formId, Title = "Test Form" };
        var questions = new List<Question>
        {
            new Question { Id = "q1", FormId = formId },
            new Question { Id = "q2", FormId = formId }
        };

        _repositoryMock.Setup(x => x.FindOrThrow(formId)).Returns(form);
        _questionServiceMock.Setup(x => x.IndexByFormId(formId)).Returns(questions);

        // Act
        var result = _formService.FetchWithQuestions(formId);

        // Assert
        result.Should().NotBeNull();
        result.form.Should().Be(form);
        result.questions.Should().HaveCount(2);
    }

    [Fact]
    public void Create_WithValidRequest_ShouldCreateForm()
    {
        // Arrange
        var request = new FormRequest { Title = "New Form" };
        var form = new Form { Id = "form123", Title = "New Form" };

        _mapperMock.Setup(x => x.Map<Form>(request)).Returns(form);
        _repositoryMock.Setup(x => x.Create(form)).Returns(form);

        // Act
        var result = _formService.Create(request);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("New Form");
        _repositoryMock.Verify(x => x.Create(form), Times.Once);
    }

    [Fact]
    public void Update_WithPublishedForm_ShouldThrowConflictException()
    {
        // Arrange
        var formId = "form123";
        var request = new FormRequest { Title = "Updated Form" };
        var form = new Form { Id = formId, Title = "Old Form", FormStatus = FormStatus.Published };

        _repositoryMock.Setup(x => x.FindOrThrow(formId)).Returns(form);

        // Act & Assert
        var exception = Assert.Throws<ConflictException>(() => _formService.Update(formId, request));
        exception.Message.Should().Contain("cannot be changed");
    }

    [Fact]
    public void Delete_WithValidId_ShouldDeleteFormAndQuestions()
    {
        // Arrange
        var formId = "form123";

        // Act
        _formService.Delete(formId);

        // Assert
        _questionServiceMock.Verify(x => x.DeleteByFormId(formId), Times.Once);
        _repositoryMock.Verify(x => x.Delete(formId), Times.Once);
    }
}
