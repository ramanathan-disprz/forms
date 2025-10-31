using AutoMapper;
using FluentAssertions;
using forms.AWS;
using forms.GraphQL.Mutations;
using forms.Request.FormSubmission;
using forms.Service.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Mutations;

public class FormSubmissionMutationTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly AmazonS3Helper _s3Helper;
    private readonly Mock<IFormSubmissionService> _serviceMock;
    private readonly Mock<ILogger<FormSubmissionMutation>> _loggerMock;
    private readonly FormSubmissionMutation _mutation;

    public FormSubmissionMutationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _s3Helper = null!; // We'll test without S3 for simplicity
        _serviceMock = new Mock<IFormSubmissionService>();
        _loggerMock = new Mock<ILogger<FormSubmissionMutation>>();
        _mutation = new FormSubmissionMutation(
            _mapperMock.Object,
            _s3Helper,
            _serviceMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void SubmitForm_ShouldReturnTrue()
    {
        // Arrange
        var request = new FormSubmissionRequest { FormId = "1", UserId = 1 };
        _serviceMock.Setup(x => x.SubmitForm(request));

        // Act
        var result = _mutation.SubmitForm(request);

        // Assert
        result.Should().BeTrue();
        _serviceMock.Verify(x => x.SubmitForm(request), Times.Once);
    }

    [Fact]
    public void DeleteFormSubmission_ShouldReturnTrue()
    {
        // Arrange
        _serviceMock.Setup(x => x.Delete(1));

        // Act
        var result = _mutation.DeleteFormSubmission(1);

        // Assert
        result.Should().BeTrue();
        _serviceMock.Verify(x => x.Delete(1), Times.Once);
    }
}
