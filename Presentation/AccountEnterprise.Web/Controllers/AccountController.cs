using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Web.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] AccountParameters parameters)
    {
        var accounts = await _mediator.Send(new GetAccountsQuery(parameters));

        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account is null)
        {
            return NotFound($"Account with id {id} is not found.");
        }
        
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountForCreationDto? account)
    {
        if (account is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateAccountCommand(account));

        return CreatedAtAction(nameof(Create), account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AccountForUpdateDto? account)
    {
        if (account is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateAccountCommand(account));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteAccountCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
        }

        return NoContent();
    }
}
