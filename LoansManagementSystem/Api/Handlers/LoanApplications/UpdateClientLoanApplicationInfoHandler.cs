using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class UpdateClientLoanApplicationInfoHandler : IRequestHandler<UpdateClientLoanApplicationInfoRequest, bool>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;
    private readonly IJwt _jwt;

    public UpdateClientLoanApplicationInfoHandler(ILoansSystem loansSystem,
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

    public async Task<bool> Handle(UpdateClientLoanApplicationInfoRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<LoanApplication>(request.ClientLoanApplicationRequest);

        result.ClientId = new Guid(_jwt.UserId);
        await _loansSystem.LoanApplications.Update(result);
        await _loansSystem.CompleteAsync();

        return true;
    }
}