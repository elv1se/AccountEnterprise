using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Domain.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;
using System.Text.Json;

namespace OperationEnterprise.WebMVC.Controllers;

[Authorize]
public class OperationsController : Controller
{
    private readonly IMediator _mediator;

    public OperationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] OperationParameters operationParameters)
    {

        var operationTypes = await _mediator.Send(new GetOperationTypesQuery());

        if (operationTypes != null)
            ViewData["OperationTypeId"] = new SelectList(operationTypes, "OperationTypeId", "Name");
        
        var pagedResult = await _mediator.Send(new GetOperationsQuery(operationParameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchType"] = operationParameters.SearchType;
        ViewData["SearchMonth"] = operationParameters.SearchMonth;
        return View(pagedResult);
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var operation = await _mediator.Send(new GetOperationByIdQuery(id));

        if (operation is null)
        {
            return NotFound($"Operation with id {id} is not found.");
        }
        
        return View(operation);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery(new()));

        if (categories != null)
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "Name");

        var operationTypes = await _mediator.Send(new GetOperationTypesQuery());

        if (operationTypes != null)
            ViewData["OperationTypeId"] = new SelectList(operationTypes, "OperationTypeId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] OperationForCreationDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateOperationCommand(employee));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetOperationByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        OperationForUpdateDto model = new()
        {
            Date = isEntityFound.Date,
            Amount = isEntityFound.Amount,
            CategoryId = isEntityFound.CategoryId,
            Name = isEntityFound.Name,
            OperationTypeId = isEntityFound.OperationTypeId,
        };  

        var categories = await _mediator.Send(new GetCategoriesQuery(new()));

        if (categories != null)
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "Name");

        var operationTypes = await _mediator.Send(new GetOperationTypesQuery());

        if (operationTypes != null)
            ViewData["OperationTypeId"] = new SelectList(operationTypes, "OperationTypeId", "Name");

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] OperationForUpdateDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateOperationCommand(employee));

        if (!isEntityFound)
        {
            return NotFound($"Operation with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetOperationByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteOperationCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Operation with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
