using AutoMapper;
using FluentAssertions;
using forms.Dto.FormAuthoring;
using forms.GraphQL.Mutations;
using forms.Model.FormAuthoring;
using forms.Request.FormAuthoring;
using forms.Service.Interface;
using Moq;
using Xunit;

namespace forms.Test.GraphQL.Mutations;

public class FormMutationTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IFormService> _serviceMock;
    private readonly FormMutation _mutation;

    public FormMutationTests()
    {
        _mapperMock = new Mock<IMapper>();
        _serviceMock = new Mock<IFormService>();
        _mutation = new FormMutation(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact]
    public void CreateForm_ShouldReturnFormDto()
    {
        // Arrange
        var request = new FormRequest { Title = "Test Form" };
        var form = new Form { Id = "1", Title = "Test Form" };
        var formDto = new FormDto { Id = "1", Title = "Test Form" };

        _serviceMock.Setup(x => x.Create(request)).Returns(form);
        _mapperMock.Setup(x => x.Map<FormDto>(form)).Returns(formDto);

        // Act
        var result = _mutation.CreateForm(request);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Form");
        _serviceMock.Verify(x => x.Create(request), Times.Once);
    }

    [Fact]
    public void UpdateForm_ShouldReturnUpdatedFormDto()
    {
        // Arrange
        var request = new FormRequest { Title = "Updated Form" };
        var form = new Form { Id = "1", Title = "Updated Form" };
        var formDto = new FormDto { Id = "1", Title = "Updated Form" };

        _serviceMock.Setup(x => x.Update("1", request)).Returns(form);
        _mapperMock.Setup(x => x.Map<FormDto>(form)).Returns(formDto);

        // Act
        var result = _mutation.UpdateForm("1", request);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Updated Form");
        _serviceMock.Verify(x => x.Update("1", request), Times.Once);
    }

    [Fact]
    public void DeleteForm_ShouldReturnTrue()
    {
        // Arrange
        _serviceMock.Setup(x => x.Delete("1"));

        // Act
        var result = _mutation.DeleteForm("1");

        // Assert
        result.Should().BeTrue();
        _serviceMock.Verify(x => x.Delete("1"), Times.Once);
    }
}
