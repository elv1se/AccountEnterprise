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
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());

        return View(categories);
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        if (category is null)
        {
            return NotFound($"Category with id {id} is not found.");
        }
        
        return View(category);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryForCreationDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateCategoryCommand(department));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var isEntityFound = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (isEntityFound == null)
        {
            return NotFound();
        }

        CategoryForUpdateDto model = new()
        {
            Name = isEntityFound.Name,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, [FromForm] CategoryForUpdateDto? department)
    {
        if (department is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateCategoryCommand(department));

        if (!isEntityFound)
        {
            return NotFound($"Category with id {id} is not found.");
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

        var achievement = await _mediator.Send(new GetCategoryByIdQuery((Guid)id));

        return View(achievement);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteCategoryCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Category with id {id} is not found.");
        }

        return RedirectToAction(nameof(Index));

    }
}
