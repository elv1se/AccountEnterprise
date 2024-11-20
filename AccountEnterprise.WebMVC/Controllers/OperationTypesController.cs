using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;

namespace AccountEnterprise.Web.Controllers;

[Authorize]
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] OperationTypeForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOperationTypeCommand(department));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetOperationTypeByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        OperationTypeForUpdateDto model = new()
        {
            Name = isEntityFound.Name,
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] OperationTypeForUpdateDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOperationTypeCommand(department));

        if (!isEntityFound)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var achievement = await _mediator.Send(new GetOperationTypeByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOperationTypeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"OperationType with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));

    }
}
