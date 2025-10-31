using AutoMapper;
using FluentAssertions;
using forms.Dto.FormAuthoring;
using forms.GraphQL.Queries;
using forms.Mapping;
using forms.Model.FormAuthoring;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Queries;

public class FormQueryTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IFormService> _serviceMock;
    private readonly FormQuery _formQuery;
    private readonly Mock<IQuestionMapper> _questionMapperMock;

    public FormQueryTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IFormService>();
        _questionMapperMock = new Mock<IQuestionMapper>();
        _formQuery = new FormQuery(_mapperMock.Object, _serviceMock.Object, _questionMapperMock.Object);
    }

    [Fact]
    public void IndexForms_ShouldReturnAllForms()
    {
        // Arrange
        var forms = new List<Form> { new Form { Id = "1" }, new Form { Id = "2" } };
        var formDtos = new List<FormDto> { new FormDto { Id = "1" }, new FormDto { Id = "2" } };

        _serviceMock.Setup(x => x.Index()).Returns(forms);
        _mapperMock.Setup(x => x.Map<IEnumerable<FormDto>>(forms)).Returns(formDtos);

        // Act
        var result = _formQuery.IndexForms();

        // Assert
        result.Should().HaveCount(2);
        _serviceMock.Verify(x => x.Index(), Times.Once);
    }

    [Fact]
    public void FetchForm_ShouldReturnForm()
    {
        // Arrange
        var form = new Form { Id = "1" };
        var formDto = new FormDto { Id = "1" };

        _serviceMock.Setup(x => x.Fetch("1")).Returns(form);
        _mapperMock.Setup(x => x.Map<FormDto>(form)).Returns(formDto);

        // Act
        var result = _formQuery.FetchForm("1");

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("1");
        _serviceMock.Verify(x => x.Fetch("1"), Times.Once);
    }

    [Fact]
    public void FetchFormWithQuestions_ShouldReturnFormWithQuestions()
    {
        // Arrange
        var formWithQuestions = new FormWithQuestions { form = new Form { Id = "1" } };
        var formWithQuestionsDto = new FormWithQuestionsDto { form = new FormDto { Id = "1" } };

        _serviceMock.Setup(x => x.FetchWithQuestions("1")).Returns(formWithQuestions);
        _mapperMock.Setup(x => x.Map<FormWithQuestionsDto>(formWithQuestions)).Returns(formWithQuestionsDto);

        // Act
        var result = _formQuery.FetchFormWithQuestions("1");

        // Assert
        result.Should().NotBeNull();
        result.form.Id.Should().Be("1");
        _serviceMock.Verify(x => x.FetchWithQuestions("1"), Times.Once);
    }
}