using AutoMapper;
using LoansManagementSystem.Api.Commands.Account;
using LoansManagementSystem.Api.Controllers;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    public AccountController(ILoansSystem loansSystem, IMapper mapper, IOptions<Configurations> config, IMediator mediator) : base(loansSystem, mapper, config, mediator) { }

    [HttpPost]
    [Route("client")]
    public async Task<IActionResult> SignClientIn([FromBody] SignInRequest account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new SignUserInRequest(account);
        var result = await _mediator.Send(command);

        if (string.IsNullOrWhiteSpace(result))
            return Unauthorized();

        return Ok(new { Token = result });
    }

    [HttpPost]
    [Route("administrator")]
    public async Task<IActionResult> SignAdminIn([FromBody] SignInRequest account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new SignUserInRequest(account);
        var result = await _mediator.Send(command);

        if (string.IsNullOrWhiteSpace(result))
            return Unauthorized();

        return Ok(new { Token = result });
    }
}