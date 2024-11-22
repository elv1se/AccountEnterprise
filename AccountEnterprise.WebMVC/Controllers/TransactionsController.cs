using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountEnterprise.Domain.RequestFeatures;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
using System.Text.Json;
<<<<<<< HEAD
>>>>>>> Добавление пагинации и поиска
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> Исправление ошибок

namespace AccountEnterprise.Web.Controllers;

[Authorize]
public class TransactionsController : Controller
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] TransactionParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetTransactionsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchType"] = parameters.SearchType;
        return View(pagedResult);
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var transaction = await _mediator.Send(new GetTransactionByIdQuery(id));

        if (transaction is null)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }
        
        return View(transaction);
    }

    [HttpGet]
    public async Task<IActionResult> Create([FromQuery] OperationParameters operationParameters)
    {
        var departments = await _mediator.Send(new GetDepartmentsQuery(new()));

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        var operations = await _mediator.Send(new GetOperationsQuery(operationParameters));

        if (operations != null)
            ViewData["OperationId"] = new SelectList(operations, "OperationId", "Name");


        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] TransactionForCreationDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateTransactionCommand(employee));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, [FromQuery] OperationParameters operationParameters)
    {
        var isEntityFound = await _mediator.Send(new GetTransactionByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        TransactionForUpdateDto model = new()
        {
            OperationId = isEntityFound.OperationId,
            Type = isEntityFound.Type,
            DepartmentId = isEntityFound.DepartmentId,
        };

        var departments = await _mediator.Send(new GetDepartmentsQuery(new()));

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");
        
        var operations = await _mediator.Send(new GetOperationsQuery(operationParameters));

        if (operations != null)
            ViewData["OperationId"] = new SelectList(operations, "OperationId", "Name");


        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] TransactionForUpdateDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateTransactionCommand(employee));

        if (!isEntityFound)
        {
            return NotFound($"Transaction with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetTransactionByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteTransactionCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Transaction with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
