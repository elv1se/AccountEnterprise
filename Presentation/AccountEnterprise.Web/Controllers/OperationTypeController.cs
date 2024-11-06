using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

[Route("api/operationTypes")]
[ApiController]
public class OperationTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var operationTypes = await _mediator.Send(new GetOperationTypesQuery());

        return Ok(operationTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var operationType = await _mediator.Send(new GetOperationTypeByIdQuery(id));

        if (operationType is null)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }
        
        return Ok(operationType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OperationTypeForCreationDto? operationType)
    {
        if (operationType is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOperationTypeCommand(operationType));

        return CreatedAtAction(nameof(Create), operationType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OperationTypeForUpdateDto? operationType)
    {
        if (operationType is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOperationTypeCommand(operationType));

        if (!isEntityFound)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOperationTypeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }

        return NoContent();
    }
}
