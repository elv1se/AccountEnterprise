using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Web.Controllers;

namespace AccountEnterprise.Tests.ControllersTests;

public class OperationTypeControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OperationTypeController _controller;

    public OperationTypeControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new OperationTypeController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfOperationTypes()
    {
        // Arrange
        var operationTypes = new List<OperationTypeDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetOperationTypesQuery(), CancellationToken.None))
            .ReturnsAsync(operationTypes);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<OperationTypeDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(operationTypes);

        _mediatorMock.Verify(m => m.Send(new GetOperationTypesQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingOperationTypeId_ReturnsOperationType()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();
        var operationType = new OperationTypeDto { Id = operationTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetOperationTypeByIdQuery(operationTypeId), CancellationToken.None))
            .ReturnsAsync(operationType);

        // Act
        var result = await _controller.GetById(operationTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as OperationTypeDto).Should().BeEquivalentTo(operationType);

        _mediatorMock.Verify(m => m.Send(new GetOperationTypeByIdQuery(operationTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingOperationTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();
        var operationType = new OperationTypeDto { Id = operationTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetOperationTypeByIdQuery(operationTypeId), CancellationToken.None))
            .ReturnsAsync((OperationTypeDto?)null);

        // Act
        var result = await _controller.GetById(operationTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetOperationTypeByIdQuery(operationTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_OperationType_ReturnsOperationType()
    {
        // Arrange
        var operationType = new OperationTypeForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateOperationTypeCommand(operationType), CancellationToken.None));

        // Act
        var result = await _controller.Create(operationType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as OperationTypeForCreationDto).Should().BeEquivalentTo(operationType);

        _mediatorMock.Verify(m => m.Send(new CreateOperationTypeCommand(operationType), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateOperationTypeCommand(It.IsAny<OperationTypeForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingOperationType_ReturnsNoContentResult()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();
        var operationType = new OperationTypeForUpdateDto { Id = operationTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOperationTypeCommand(operationType), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(operationTypeId, operationType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationTypeCommand(operationType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingOperationType_ReturnsNotFoundResult()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();
        var operationType = new OperationTypeForUpdateDto { Id = operationTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateOperationTypeCommand(operationType), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(operationTypeId, operationType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationTypeCommand(operationType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(operationTypeId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateOperationTypeCommand(It.IsAny<OperationTypeForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingOperationTypeId_ReturnsNoContentResult()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOperationTypeCommand(operationTypeId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(operationTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteOperationTypeCommand(operationTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingOperationTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var operationTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteOperationTypeCommand(operationTypeId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(operationTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteOperationTypeCommand(operationTypeId), CancellationToken.None), Times.Once);
    }
}

