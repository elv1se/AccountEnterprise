using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

public class TransactionsController : Controller
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var transactions = await _mediator.Send(new GetTransactionsQuery());

        return View(transactions);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));

        if (transaction is null)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }
        
        return View(transaction);
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

    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, [FromBody] TransactionForUpdateDto? transaction)
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

    [HttpDelete]
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
