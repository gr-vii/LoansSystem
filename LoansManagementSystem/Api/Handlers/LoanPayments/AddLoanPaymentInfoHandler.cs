using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanPayments;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanPayments;

public class AddLoanPaymentInfoHandler : IRequestHandler<CreateLoanPaymentInfoRequest, LoanPayment>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public AddLoanPaymentInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<LoanPayment> Handle(CreateLoanPaymentInfoRequest request, CancellationToken cancellationToken)
    {
        var loanPayment = _mapper.Map<LoanPayment>(request.LoanPaymentRequest);

        await _loansSystem.LoanPayment.Add(loanPayment);
        await _loansSystem.CompleteAsync();

        return _mapper.Map<LoanPayment>(loanPayment);
    }
}