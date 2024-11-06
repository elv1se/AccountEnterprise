using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery());

        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));

        if (transaction is null)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }
        
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionForCreationDto? transaction)
    {
        if (transaction is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateTransactionCommand(transaction));

        return CreatedAtAction(nameof(Create), transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TransactionForUpdateDto? transaction)
    {
        if (transaction is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateTransactionCommand(transaction));

        if (!isEntityFound)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteTransactionCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }

        return NoContent();
    }
}
