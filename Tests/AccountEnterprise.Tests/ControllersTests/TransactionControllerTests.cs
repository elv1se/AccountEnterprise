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

public class TransactionControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TransactionController _controller;

    public TransactionControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TransactionController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfTransactions()
    {
        // Arrange
        var transactions = new List<TransactionDto> { new(), new() };

        _mediatorMock
            .Setup(m => m.Send(new GetTransactionsQuery(), CancellationToken.None))
            .ReturnsAsync(transactions);

        // Act
        var result = await _controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<TransactionDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(transactions);

        _mediatorMock.Verify(m => m.Send(new GetTransactionsQuery(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingTransactionId_ReturnsTransaction()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionDto { Id = transactionId };

        _mediatorMock
            .Setup(m => m.Send(new GetTransactionByIdQuery(transactionId), CancellationToken.None))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.GetById(transactionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as TransactionDto).Should().BeEquivalentTo(transaction);

        _mediatorMock.Verify(m => m.Send(new GetTransactionByIdQuery(transactionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingTransactionId_ReturnsNotFoundResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionDto { Id = transactionId };

        _mediatorMock
            .Setup(m => m.Send(new GetTransactionByIdQuery(transactionId), CancellationToken.None))
            .ReturnsAsync((TransactionDto?)null);

        // Act
        var result = await _controller.GetById(transactionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetTransactionByIdQuery(transactionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Transaction_ReturnsTransaction()
    {
        // Arrange
        var transaction = new TransactionForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateTransactionCommand(transaction), CancellationToken.None));

        // Act
        var result = await _controller.Create(transaction);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as TransactionForCreationDto).Should().BeEquivalentTo(transaction);

        _mediatorMock.Verify(m => m.Send(new CreateTransactionCommand(transaction), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateTransactionCommand(It.IsAny<TransactionForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingTransaction_ReturnsNoContentResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionForUpdateDto { Id = transactionId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTransactionCommand(transaction), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(transactionId, transaction);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateTransactionCommand(transaction), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingTransaction_ReturnsNotFoundResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionForUpdateDto { Id = transactionId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTransactionCommand(transaction), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(transactionId, transaction);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateTransactionCommand(transaction), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var transactionId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(transactionId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateTransactionCommand(It.IsAny<TransactionForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingTransactionId_ReturnsNoContentResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTransactionCommand(transactionId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(transactionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteTransactionCommand(transactionId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingTransactionId_ReturnsNotFoundResult()
    {
        // Arrange
        var transactionId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTransactionCommand(transactionId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(transactionId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteTransactionCommand(transactionId), CancellationToken.None), Times.Once);
    }
}

