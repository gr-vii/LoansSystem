using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class UpdateClientLoanApplicationInfoHandler : IRequestHandler<UpdateClientLoanApplicationInfoRequest, bool>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public UpdateClientLoanApplicationInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<bool> Handle(UpdateClientLoanApplicationInfoRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<LoanApplication>(request.ClientLoanApplicationRequest);

        await _loansSystem.LoanApplications.Update(result);
        await _loansSystem.CompleteAsync();

        return true;
    }
}