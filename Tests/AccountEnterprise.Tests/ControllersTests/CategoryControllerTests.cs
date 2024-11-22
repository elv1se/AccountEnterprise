using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Web.Controllers;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Tests.ControllersTests;

public class CategoryControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CategoryController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfCategories()
    {
        // Arrange
        var categories = new PagedList<CategoryDto>([new(), new()], 1, 1, 1);
        var parameters = new CategoryParameters();
        _mediatorMock
            .Setup(m => m.Send(new GetCategoriesQuery(parameters), CancellationToken.None))
            .ReturnsAsync(categories);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<CategoryDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(categories);

        _mediatorMock.Verify(m => m.Send(new GetCategoriesQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingCategoryId_ReturnsCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new CategoryDto { CategoryId = categoryId };

        _mediatorMock
            .Setup(m => m.Send(new GetCategoryByIdQuery(categoryId), CancellationToken.None))
            .ReturnsAsync(category);

        // Act
        var result = await _controller.GetById(categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as CategoryDto).Should().BeEquivalentTo(category);

        _mediatorMock.Verify(m => m.Send(new GetCategoryByIdQuery(categoryId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingCategoryId_ReturnsNotFoundResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new CategoryDto { CategoryId = categoryId };

        _mediatorMock
            .Setup(m => m.Send(new GetCategoryByIdQuery(categoryId), CancellationToken.None))
            .ReturnsAsync((CategoryDto?)null);

        // Act
        var result = await _controller.GetById(categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetCategoryByIdQuery(categoryId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Category_ReturnsCategory()
    {
        // Arrange
        var category = new CategoryForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateCategoryCommand(category), CancellationToken.None));

        // Act
        var result = await _controller.Create(category);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as CategoryForCreationDto).Should().BeEquivalentTo(category);

        _mediatorMock.Verify(m => m.Send(new CreateCategoryCommand(category), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateCategoryCommand(It.IsAny<CategoryForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingCategory_ReturnsNoContentResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new CategoryForUpdateDto { Id = categoryId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateCategoryCommand(category), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(categoryId, category);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateCategoryCommand(category), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingCategory_ReturnsNotFoundResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new CategoryForUpdateDto { Id = categoryId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateCategoryCommand(category), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(categoryId, category);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateCategoryCommand(category), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(categoryId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateCategoryCommand(It.IsAny<CategoryForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingCategoryId_ReturnsNoContentResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteCategoryCommand(categoryId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteCategoryCommand(categoryId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingCategoryId_ReturnsNotFoundResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteCategoryCommand(categoryId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteCategoryCommand(categoryId), CancellationToken.None), Times.Once);
    }
}

