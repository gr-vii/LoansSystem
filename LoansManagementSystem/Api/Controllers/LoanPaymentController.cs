using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanPayments;
using LoansManagementSystem.Api.Queries.LoanPayments;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoanPaymentController : BaseController
{
    public LoanPaymentController(ILoansSystem loansSystem, IMapper mapper, IOptions<Configurations> config, IMediator mediator) : base(loansSystem, mapper, config, mediator) { }

    [HttpGet]
    [Route("{loanId:guid}")]
    public async Task<IActionResult> GetLoanPayments(Guid loanId)
    {
        var query = new GetAllLoanPaymentsQuery(loanId);
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddLoanPayment([FromBody] CreateLoanPaymentRequest loanPayment)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        //TODO:Vlidate all the previous amount so we dont have a number in -
        //TODO: update term to mean months instead of number of payments
        var command = new CreateLoanPaymentInfoRequest(loanPayment);
        var result = await _mediator.Send(command);

        return Created(nameof(GetLoanPayments), new { loanId = result.LoanId });
    }
}