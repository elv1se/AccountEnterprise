using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

public class OperationsController : Controller
{
    private readonly IMediator _mediator;

    public OperationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var operations = await _mediator.Send(new GetOperationsQuery());

        return View(operations);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var operation = await _mediator.Send(new GetOperationByIdQuery(id));

        if (operation is null)
        {
            return NotFound($"Operation with id {id} is not found.");
        }
        
        return View(operation);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OperationForCreationDto? operation)
    {
        if (operation is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOperationCommand(operation));

        return CreatedAtAction(nameof(Create), operation);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, [FromBody] OperationForUpdateDto? operation)
    {
        if (operation is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOperationCommand(operation));

        if (!isEntityFound)
        {
            return NotFound($"Operation with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOperationCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Operation with id {id} is not found.");
        }

        return NoContent();
    }
}
