using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountEnterprise.Domain.RequestFeatures;
using System.Text.Json;

namespace AccountEnterprise.WebMVC.Controllers;

[Authorize]
public class AccountsController : Controller
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index([FromQuery] AccountParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetAccountsQuery(parameters));
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        ViewData["SearchNumber"] = parameters.SearchNumber;
        return View(pagedResult);
    }


    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var account = await _mediator.Send(new GetAccountByIdQuery(id));

        if (account is null)
        {
            return NotFound($"Account with id {id} is not found.");
        }
        
        return View(account);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var departments = await _mediator.Send(new GetDepartmentsQuery(new()));

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromForm] AccountForCreationDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateAccountCommand(employee));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetAccountByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        AccountForUpdateDto model = new()
        {
            BankName = isEntityFound.BankName,
            Type = isEntityFound.Type,
            DepartmentId = isEntityFound.DepartmentId,
            Number = isEntityFound.Number
        };

        var departments = await _mediator.Send(new GetDepartmentsQuery(new()));

        if (departments != null)
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name");

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(Guid id, [FromForm] AccountForUpdateDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateAccountCommand(employee));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetAccountByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteAccountCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));
    }
}
