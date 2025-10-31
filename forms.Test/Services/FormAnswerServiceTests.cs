using AutoMapper;
using FluentAssertions;
using forms.Model.FormSubmission;
using forms.Repository.Interfaces;
using forms.Request.FormSubmission;
using forms.Service.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Tests.Services;

public class FormAnswerServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<FormAnswerService>> _loggerMock;
    private readonly Mock<IFormAnswerRepository> _repositoryMock;
    private readonly FormAnswerService _formAnswerService;

    public FormAnswerServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<FormAnswerService>>();
        _repositoryMock = new Mock<IFormAnswerRepository>();
        _formAnswerService = new FormAnswerService(
            _mapperMock.Object,
            _loggerMock.Object,
            _repositoryMock.Object
        );
    }

    [Fact]
    public void IndexBySubmissionId_ShouldReturnAnswers()
    {
        // Arrange
        var submissionId = 1L;
        var answers = new List<FormAnswer>
        {
            new FormAnswer 
            { 
                Id = 1, 
                SubmissionId = submissionId, 
                QuestionId = "q1",
                QuestionType = "ShortText",
                ValueText = "Answer 1"
            },
            new FormAnswer 
            { 
                Id = 2, 
                SubmissionId = submissionId, 
                QuestionId = "q2",
                QuestionType = "Number",
                ValueText = "42"
            }
        };

        _repositoryMock.Setup(x => x.IndexAllBySubmissionId(submissionId))
            .Returns(answers);

        // Act
        var result = _formAnswerService.IndexBySubmissionId(submissionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().SubmissionId.Should().Be(submissionId);
        _repositoryMock.Verify(x => x.IndexAllBySubmissionId(submissionId), Times.Once);
    }

    [Fact]
    public void CreateMany_WithValidRequests_ShouldCreateAnswers()
    {
        // Arrange
        var submissionId = 1L;
        var requests = new List<FormAnswerRequest>
        {
            new FormAnswerRequest 
            { 
                SubmissionId = submissionId,
                QuestionId = "q1",
                QuestionType = forms.Enum.QuestionType.ShortText,
                ValueText = "Answer 1"
            },
            new FormAnswerRequest 
            { 
                SubmissionId = submissionId,
                QuestionId = "q2",
                QuestionType = forms.Enum.QuestionType.Number,
                ValueText = "42"
            }
        };

        var mappedAnswers = new List<FormAnswer>
        {
            new FormAnswer 
            { 
                SubmissionId = submissionId,
                QuestionId = "q1",
                QuestionType = "ShortText",
                ValueText = "Answer 1"
            },
            new FormAnswer 
            { 
                SubmissionId = submissionId,
                QuestionId = "q2",
                QuestionType = "Number",
                ValueText = "42"
            }
        };

        var createdAnswers = mappedAnswers.Select(a => 
        {
            a.Id = new Random().Next(1000, 9999);
            return a;
        }).ToList();

        _mapperMock.Setup(x => x.Map<IEnumerable<FormAnswer>>(requests))
            .Returns(mappedAnswers);

        _repositoryMock.Setup(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()))
            .Returns(createdAnswers);

        // Act
        var result = _formAnswerService.CreateMany(requests);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        _repositoryMock.Verify(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()), Times.Once);
    }

    [Fact]
    public void CreateMany_ShouldGenerateIdForEachAnswer()
    {
        // Arrange
        var requests = new List<FormAnswerRequest>
        {
            new FormAnswerRequest 
            { 
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueText = "Answer"
            }
        };

        var mappedAnswers = new List<FormAnswer>
        {
            new FormAnswer 
            { 
                Id = 0, // No ID initially
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueText = "Answer"
            }
        };

        _mapperMock.Setup(x => x.Map<IEnumerable<FormAnswer>>(requests))
            .Returns(mappedAnswers);

        FormAnswer capturedAnswer = null;
        _repositoryMock.Setup(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()))
            .Callback<IEnumerable<FormAnswer>>(answers => capturedAnswer = answers.First())
            .Returns((IEnumerable<FormAnswer> answers) => answers);

        // Act
        _formAnswerService.CreateMany(requests);

        // Assert
        capturedAnswer.Should().NotBeNull();
        capturedAnswer.Id.Should().NotBe(0); // ID should be generated
    }

    [Fact]
    public void CreateMany_WithEmptyValueJson_ShouldSetToNull()
    {
        // Arrange
        var requests = new List<FormAnswerRequest>
        {
            new FormAnswerRequest 
            { 
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueJson = "   " // Whitespace only
            }
        };

        var mappedAnswers = new List<FormAnswer>
        {
            new FormAnswer 
            { 
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueJson = "   "
            }
        };

        _mapperMock.Setup(x => x.Map<IEnumerable<FormAnswer>>(requests))
            .Returns(mappedAnswers);

        FormAnswer capturedAnswer = null;
        _repositoryMock.Setup(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()))
            .Callback<IEnumerable<FormAnswer>>(answers => capturedAnswer = answers.First())
            .Returns((IEnumerable<FormAnswer> answers) => answers);

        // Act
        _formAnswerService.CreateMany(requests);

        // Assert
        capturedAnswer.Should().NotBeNull();
        capturedAnswer.ValueJson.Should().BeNull();
    }

    [Fact]
    public void CreateMany_WithSingleRequest_ShouldCreateAnswer()
    {
        // Arrange
        var requests = new List<FormAnswerRequest>
        {
            new FormAnswerRequest 
            { 
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueText = "Test Answer"
            }
        };

        var mappedAnswers = new List<FormAnswer>
        {
            new FormAnswer 
            { 
                SubmissionId = 1L,
                QuestionId = "q1",
                ValueText = "Test Answer"
            }
        };

        _mapperMock.Setup(x => x.Map<IEnumerable<FormAnswer>>(requests))
            .Returns(mappedAnswers);

        _repositoryMock.Setup(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()))
            .Returns(mappedAnswers);

        // Act
        var result = _formAnswerService.CreateMany(requests);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        _repositoryMock.Verify(x => x.CreateMany(It.IsAny<IEnumerable<FormAnswer>>()), Times.Once);
    }

    [Fact]
    public void IndexBySubmissionId_WithNoAnswers_ShouldReturnEmptyList()
    {
        // Arrange
        var submissionId = 999L;
        var emptyAnswers = new List<FormAnswer>();

        _repositoryMock.Setup(x => x.IndexAllBySubmissionId(submissionId))
            .Returns(emptyAnswers);

        // Act
        var result = _formAnswerService.IndexBySubmissionId(submissionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        _repositoryMock.Verify(x => x.IndexAllBySubmissionId(submissionId), Times.Once);
    }
}
