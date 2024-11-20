using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var departments = await _mediator.Send(new GetDepartmentsQuery());

        return View(departments);
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var department = await _mediator.Send(new GetDepartmentByIdQuery(id));

        if (department is null)
        {
            return NotFound($"Department with id {id} is not found.");
        }
        
        return View(department);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] DepartmentForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentCommand(department));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetDepartmentByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        DepartmentForUpdateDto model = new()
        {
            Name = isEntityFound.Name,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] DepartmentForUpdateDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateDepartmentCommand(department));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetDepartmentByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));

    }
}
