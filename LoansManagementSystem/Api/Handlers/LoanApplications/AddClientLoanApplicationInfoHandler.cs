using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.MessageQueue;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class AddClientLoanApplicationInfoHandler : IRequestHandler<CreateClientLoanApplicationInfoRequest, Guid>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;
    protected readonly IMessageProducer _messageProducer;

    public AddClientLoanApplicationInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IMessageProducer messageProducer
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
        _messageProducer = messageProducer;
    }

    public async Task<Guid> Handle(CreateClientLoanApplicationInfoRequest request, CancellationToken cancellationToken)
    {
        //var validationResult = _loansSystem.ValidateLoanApplication(request.ClientLoanApplicationRequest);

        //if (!string.IsNullOrWhiteSpace(validationResult.ErrorMessage))
        //    throw new BadHttpRequestException(validationResult.ErrorMessage);

        var result = _mapper.Map<LoanApplication>(request.ClientLoanApplicationRequest);

        await _loansSystem.LoanApplications.Add(result);
        await _loansSystem.CompleteAsync();

        _messageProducer.SendMessage(result);

        return result.Id;
    }
}