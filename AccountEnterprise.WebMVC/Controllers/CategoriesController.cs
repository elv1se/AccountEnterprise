using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;
public class CategoriesController : Controller
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());

        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        if (category is null)
        {
            return NotFound($"Category with id {id} is not found.");
        }
        
        return View(category);
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

    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, [FromBody] CategoryForUpdateDto? category)
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

    [HttpDelete]
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
