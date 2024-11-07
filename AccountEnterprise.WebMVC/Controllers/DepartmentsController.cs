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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateDepartmentCommand(department));

        return CreatedAtAction(nameof(Create), department);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, [FromBody] DepartmentForUpdateDto? department)
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

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteDepartmentCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Department with id {id} is not found.");
        }

        return NoContent();
    }
}
