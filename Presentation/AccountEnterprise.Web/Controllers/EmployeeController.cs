﻿using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;
using AccountEnterprise.Domain.RequestFeatures;

namespace AccountEnterprise.Web.Controllers;

[Route("api/employees")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] EmployeeParameters parameters)
    {
        var employees = await _mediator.Send(new GetEmployeesQuery(parameters));

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

        if (employee is null)
        {
            return NotFound($"Employee with id {id} is not found.");
        }
        
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeForCreationDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateEmployeeCommand(employee));

        return CreatedAtAction(nameof(Create), employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeForUpdateDto? employee)
    {
        if (employee is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateEmployeeCommand(employee));

        if (!isEntityFound)
        {
            return NotFound($"Employee with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteEmployeeCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Employee with id {id} is not found.");
        }

        return NoContent();
    }
}
