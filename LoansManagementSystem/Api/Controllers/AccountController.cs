using AutoMapper;
using LoansManagementSystem.Api.Commands.Account;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    public AccountController(ILoansSystem loansSystem, IMapper mapper, IOptions<Configurations> config, IMediator mediator) : base(loansSystem, mapper, config, mediator) { }

    [HttpPost]
    [Route("client")]
    public async Task<IActionResult> SignClientIn([FromBody] SignClientInRequest account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new SignClientInfoInRequest(account);
        var result = await _mediator.Send(command);

        if (string.IsNullOrWhiteSpace(result))
            return Unauthorized();

        return Ok(new { Token = result });
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("administrator")]
    public async Task<IActionResult> SignAdminIn([FromBody] SignAdministratorInRequest account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new SignAdministratorInfoInRequest(account);
        var result = await _mediator.Send(command);

        if (string.IsNullOrWhiteSpace(result))
            return Unauthorized();

        return Ok(new { Token = result });
    }
}