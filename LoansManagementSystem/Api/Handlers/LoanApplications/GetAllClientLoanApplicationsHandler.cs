using AutoMapper;
using LoansManagementSystem.Api.Queries.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class GetAllClientLoanApplicationsHandler : IRequestHandler<GetAllClientLoanApplicationsQuery, IEnumerable<ClientLoanApplicationResponse>>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public GetAllClientLoanApplicationsHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<IEnumerable<ClientLoanApplicationResponse>> Handle(GetAllClientLoanApplicationsQuery request, CancellationToken cancellationToken)
    {
        var ClientLoans = await _loansSystem.LoanApplications.GetClientLoansAsync(request.ClientId);
        return _mapper.Map<IEnumerable<ClientLoanApplicationResponse>>(ClientLoans);
    }
}