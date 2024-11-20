using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Web.Controllers;

[Route("api/operations")]
[ApiController]
public class OperationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] OperationParameters operationParameters)
    {
        var operations = await _mediator.Send(new GetOperationsQuery(operationParameters));

        return Ok(operations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var operation = await _mediator.Send(new GetOperationByIdQuery(id));

        if (operation is null)
        {
            return NotFound($"Operation with id {id} is not found.");
        }
        
        return Ok(operation);
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

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
