using AutoMapper;
using FluentAssertions;
using forms.Dto.FormSubmission;
using forms.GraphQL.Queries;
using forms.Model.FormSubmission;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Queries;

public class FormSubmissionQueryTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IFormSubmissionService> _serviceMock;
    private readonly FormSubmissionQuery _query;

    public FormSubmissionQueryTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IFormSubmissionService>();
        _query = new FormSubmissionQuery(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public void FetchFormSubmission_ShouldReturnSubmission()
    {
        // Arrange
        var detail = new FormSubmissionDetail 
        { 
            Submission = new FormSubmission { Id = 1 },
            Answers = new List<FormAnswer>()
        };
        var dto = new FormSubmissionDetailDto 
        { 
            Submission = new FormSubmissionDto { Id = "1" },
            Answers = new List<FormAnswerDto>()
        };
        
        _serviceMock.Setup(x => x.Fetch(1, false)).Returns(detail);
        _mapperMock.Setup(x => x.Map<FormSubmissionDetailDto>(detail)).Returns(dto);

        // Act
        var result = _query.FetchFormSubmission(1);

        // Assert
        result.Should().NotBeNull();
        result.Submission.Id.Should().Be("1");
        _serviceMock.Verify(x => x.Fetch(1, false), Times.Once);
    }

    [Fact]
    public void IndexFormSubmissionsByFormId_ShouldReturnSubmissions()
    {
        // Arrange
        var submissions = new List<FormSubmission> { new FormSubmission { FormId = "form1" } };
        var dtos = new List<FormSubmissionDto> { new FormSubmissionDto { FormId = "form1" } };
        
        _serviceMock.Setup(x => x.IndexByFormId("form1")).Returns(submissions);
        _mapperMock.Setup(x => x.Map<IEnumerable<FormSubmissionDto>>(submissions)).Returns(dtos);

        // Act
        var result = _query.IndexFormSubmissionByFormId("form1");

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        _serviceMock.Verify(x => x.IndexByFormId("form1"), Times.Once);
    }
}
