using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanPayments;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanPayments;

public class AddLoanPaymentInfoHandler : IRequestHandler<CreateLoanPaymentInfoRequest, LoanPaymentProcessingResponse>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;
    private readonly IJwt _jwt;

    public AddLoanPaymentInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IJwt jwt
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
        _jwt = jwt;
    }

    public async Task<LoanPaymentProcessingResponse> Handle(CreateLoanPaymentInfoRequest request, CancellationToken cancellationToken)
    {
        var loanPayment = _mapper.Map<LoanPayment>(request.LoanPaymentRequest);

        var previousPayments = await _loansSystem.LoanPayments.GetLoanPaymentsAsync(loanPayment.LoanId);
        var previousPaymentsTotal = previousPayments.Sum(e => e.Amount);

        var loan = await _loansSystem.LoanApplications.GetById(loanPayment.LoanId);

        var totalAmountDue = (loan.Amount + (_config.LoanInterestRate * loan.Amount / 100)) - previousPaymentsTotal;
        //100 >= 2000
        if (loanPayment.Amount >= totalAmountDue)
        {
            loanPayment.Amount = totalAmountDue;
        }

        await _loansSystem.LoanPayments.Add(loanPayment);
        await _loansSystem.CompleteAsync();

        var loanPaymentResponse = _mapper.Map<LoanPaymentProcessingResponse>(loanPayment);

        loanPaymentResponse.IsProcessed = true;

        return loanPaymentResponse;
    }
}