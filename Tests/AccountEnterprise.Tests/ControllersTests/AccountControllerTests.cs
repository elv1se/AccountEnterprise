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

public class AccountControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AccountController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsListOfAccounts()
    {
        // Arrange
        var accounts = new PagedList<AccountDto>([new(), new()], 1, 1, 1);

        var parameters = new AccountParameters();

        _mediatorMock
            .Setup(m => m.Send(new GetAccountsQuery(parameters), CancellationToken.None))
            .ReturnsAsync(accounts);

        // Act
        var result = await _controller.Get(parameters);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var value = okResult?.Value as List<AccountDto>;
        value.Should().HaveCount(2);
        value.Should().BeEquivalentTo(accounts);

        _mediatorMock.Verify(m => m.Send(new GetAccountsQuery(parameters), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_ExistingAccountId_ReturnsAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new AccountDto { Id = accountId };

        _mediatorMock
            .Setup(m => m.Send(new GetAccountByIdQuery(accountId), CancellationToken.None))
            .ReturnsAsync(account);

        // Act
        var result = await _controller.GetById(accountId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as AccountDto).Should().BeEquivalentTo(account);

        _mediatorMock.Verify(m => m.Send(new GetAccountByIdQuery(accountId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingAccountId_ReturnsNotFoundResult()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new AccountDto { Id = accountId };

        _mediatorMock
            .Setup(m => m.Send(new GetAccountByIdQuery(accountId), CancellationToken.None))
            .ReturnsAsync((AccountDto?)null);

        // Act
        var result = await _controller.GetById(accountId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetAccountByIdQuery(accountId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Account_ReturnsAccount()
    {
        // Arrange
        var account = new AccountForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateAccountCommand(account), CancellationToken.None));

        // Act
        var result = await _controller.Create(account);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as AccountForCreationDto).Should().BeEquivalentTo(account);

        _mediatorMock.Verify(m => m.Send(new CreateAccountCommand(account), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateAccountCommand(It.IsAny<AccountForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingAccount_ReturnsNoContentResult()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new AccountForUpdateDto { Id = accountId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateAccountCommand(account), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(accountId, account);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateAccountCommand(account), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingAccount_ReturnsNotFoundResult()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var account = new AccountForUpdateDto { Id = accountId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateAccountCommand(account), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(accountId, account);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateAccountCommand(account), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var accountId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(accountId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateAccountCommand(It.IsAny<AccountForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingAccountId_ReturnsNoContentResult()
    {
        // Arrange
        var accountId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteAccountCommand(accountId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(accountId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteAccountCommand(accountId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingAccountId_ReturnsNotFoundResult()
    {
        // Arrange
        var accountId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteAccountCommand(accountId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(accountId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteAccountCommand(accountId), CancellationToken.None), Times.Once);
    }
}

