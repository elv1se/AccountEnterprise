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

public class OperationControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OperationController _controller;

    public OperationControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OperationController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfOperations()
    {
        // Arrange
        var operations = new PagedList<OperationDto>([new(), new()], 2, 1, 5);

        var parameters = new OperationParameters();

        _mediatorMock
            .Setup(m => m.Send(new GetOperationsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(operations);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<OperationDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(operations);

        _mediatorMock.Verify(m => m.Send(new GetOperationsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingOperationId_ReturnsOperation()
    {
        // Arrange
        var operationId = Guid.NewGuid();
        var operation = new OperationDto { OperationId = operationId };

        _mediatorMock
            .Setup(m => m.Send(new GetOperationByIdQuery(operationId), CancellationToken.None))
            .ReturnsAsync(operation);

        // Act
        var result = await _controller.GetById(operationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as OperationDto).Should().BeEquivalentTo(operation);

        _mediatorMock.Verify(m => m.Send(new GetOperationByIdQuery(operationId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingOperationId_ReturnsNotFoundResult()
    {
        // Arrange
        var operationId = Guid.NewGuid();
        var operation = new OperationDto { OperationId = operationId };

        _mediatorMock
            .Setup(m => m.Send(new GetOperationByIdQuery(operationId), CancellationToken.None))
            .ReturnsAsync((OperationDto?)null);

        // Act
        var result = await _controller.GetById(operationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetOperationByIdQuery(operationId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Operation_ReturnsOperation()
    {
        // Arrange
        var operation = new OperationForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateOperationCommand(operation), CancellationToken.None));

        // Act
        var result = await _controller.Create(operation);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as OperationForCreationDto).Should().BeEquivalentTo(operation);

        _mediatorMock.Verify(m => m.Send(new CreateOperationCommand(operation), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateOperationCommand(It.IsAny<OperationForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingOperation_ReturnsNoContentResult()
    {
        // Arrange
        var operationId = Guid.NewGuid();
        var operation = new OperationForUpdateDto { Id = operationId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOperationCommand(operation), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(operationId, operation);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationCommand(operation), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingOperation_ReturnsNotFoundResult()
    {
        // Arrange
        var operationId = Guid.NewGuid();
        var operation = new OperationForUpdateDto { Id = operationId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOperationCommand(operation), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(operationId, operation);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationCommand(operation), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var operationId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(operationId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationCommand(It.IsAny<OperationForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingOperationId_ReturnsNoContentResult()
    {
        // Arrange
        var operationId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOperationCommand(operationId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(operationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteOperationCommand(operationId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingOperationId_ReturnsNotFoundResult()
    {
        // Arrange
        var operationId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOperationCommand(operationId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(operationId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteOperationCommand(operationId), CancellationToken.None), Times.Once);
    }
}

