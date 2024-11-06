using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        if (category is null)
        {
            return NotFound($"Category with id {id} is not found.");
        }
        
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryForCreationDto? category)
    {
        if (category is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateCategoryCommand(category));

        return CreatedAtAction(nameof(Create), category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryForUpdateDto? category)
    {
        if (category is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateCategoryCommand(category));

        if (!isEntityFound)
        {
            return NotFound($"Category with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteCategoryCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Category with id {id} is not found.");
        }

        return NoContent();
    }
}
