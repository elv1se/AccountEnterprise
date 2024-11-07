using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

public class OperationTypesController : Controller
{
    private readonly IMediator _mediator;

    public OperationTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var operationTypes = await _mediator.Send(new GetOperationTypesQuery());

        return View(operationTypes);
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var operationType = await _mediator.Send(new GetOperationTypeByIdQuery(id));

        if (operationType is null)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }
        
        return View(operationType);
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

    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, [FromBody] OperationTypeForUpdateDto? operationType)
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

    [HttpDelete]
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
