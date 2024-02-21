using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.Api.Queries.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Controllers;

[ApiController]
[AuthorizeClaim(new string[] { "Client" })]
[Route("api/[controller]")]
public class LoanApplicationsController : BaseController
{
    public LoanApplicationsController(ILoansSystem loansSystem, IMapper mapper, IOptions<Configurations> config, IMediator mediator) : base(loansSystem, mapper, config, mediator) { }

    [HttpGet]
    [Route("{clientId:guid}")]
    public async Task<IActionResult> GetClientLoans(Guid clientId)
    {
        var query = new GetAllClientLoanApplicationsQuery(clientId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddLoanApplication([FromBody] CreateClientLoanApplicationRequest loanApplication)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new CreateClientLoanApplicationInfoRequest(loanApplication);
        var result = await _mediator.Send(command);

        return Created(nameof(GetClientLoans), new { loanId = result });
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateLoanApplication([FromBody] UpdateClientLoanApplicationRequest loanApplication)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var command = new UpdateClientLoanApplicationInfoRequest(loanApplication);
        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest();
    }
}