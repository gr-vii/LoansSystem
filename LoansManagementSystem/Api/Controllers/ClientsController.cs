using AutoMapper;
using LoansManagementSystem.Api.Commands.Clients;
using LoansManagementSystem.Api.Queries.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace LoansManagementSystem.Api.Controllers;

[AuthorizeAdmin]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : BaseController
{
    public ClientsController(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IMediator mediator)
        : base(loansSystem, mapper, config, mediator)
    { }

    [HttpGet]
    [Route("{clientId:guid}")]
    public async Task<IActionResult> GetClient(Guid clientId)
    {
        var query = new GetClientQuery(clientId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddClient([FromBody] CreateClientRequest client)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new CreateClientInfoRequest(client);
        var result = await _mediator.Send(command);

        return Created(nameof(GetClient), new { clientId = result.Id });
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateClient([FromBody] UpdateClientRequest client)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new UpdateClientInfoRequest(client);
        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest();
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllClients()
    {
        var query = new GetAllClientsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> DeleteClient(Guid clientId)
    {
        var command = new DeleteClientInfoRequest(clientId);
        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest();
    }
}