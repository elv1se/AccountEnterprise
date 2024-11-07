using Microsoft.AspNetCore.Mvc;
﻿using MediatR;
using AccountEnterprise.Application.Dtos;
using AccountEnterprise.Application.Requests.Queries;
using AccountEnterprise.Application.Requests.Commands;

namespace AccountEnterprise.Web.Controllers;


public class AccountsController : Controller
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 294, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var accounts = await _mediator.Send(new GetAccountsQuery());
        return View(accounts);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountForCreationDto? account)
    {
        if (account is null)
        {
            return BadRequest("Object for creation is null");
        }

        await _mediator.Send(new CreateAccountCommand(account));

        return CreatedAtAction(nameof(Create), account);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(Guid id, [FromBody] AccountForUpdateDto? account)
    {
        if (account is null)
        {
            return BadRequest("Object for update is null");
        }

        var isEntityFound = await _mediator.Send(new UpdateAccountCommand(account));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isEntityFound = await _mediator.Send(new DeleteAccountCommand(id));

        if (!isEntityFound)
        {
            return NotFound($"Account with id {id} is not found.");
        }

        return NoContent();
    }
}
