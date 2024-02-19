using AutoMapper;
using LoansManagementSystem.Api.Queries.LoanPayments;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanPayments;

public class GetAllLoanPaymentsHandler : IRequestHandler<GetAllLoanPaymentsQuery, IEnumerable<LoanPaymentResponse>>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public GetAllLoanPaymentsHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<IEnumerable<LoanPaymentResponse>> Handle(GetAllLoanPaymentsQuery request, CancellationToken cancellationToken)
    {
        var LoanPayment = await _loansSystem.LoanPayments.GetLoanPaymentsAsync(request.LoanId);
        return _mapper.Map<IEnumerable<LoanPaymentResponse>>(LoanPayment);
    }
}