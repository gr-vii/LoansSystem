using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Services;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class SetStatusClientLoanApplicationInfoHandler : IRequestHandler<SetStatusClientLoanApplicationInfoRequest, bool>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;
    private readonly IEmailService _emailService;

    public SetStatusClientLoanApplicationInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IEmailService emailService)
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
        _emailService = emailService;

    }

    public async Task<bool> Handle(SetStatusClientLoanApplicationInfoRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<LoanApplication>(request.ClientLoanApplicationRequest);
        Console.WriteLine("hiiiiiiiiiiiiiiiiiiiiiii");

        _emailService.SendEmailAsync("test@9ar.com", "teat", "test");
        await _loansSystem.LoanApplications.Update(result);
        await _loansSystem.CompleteAsync();

        return true;
    }
}