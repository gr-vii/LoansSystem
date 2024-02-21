using AutoMapper;
using LoansManagementSystem.Api.Commands.LoanApplications;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.MessageQueue;
using LoansManagementSystem.Security;
using MediatR;

namespace LoansManagementSystem.Api.Handlers.LoanApplications;

public class AddClientLoanApplicationInfoHandler : IRequestHandler<CreateClientLoanApplicationInfoRequest, Guid>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly IJwt _jwt;
    protected readonly IMessageProducer _messageProducer;

    public AddClientLoanApplicationInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IJwt jwt,
        IMessageProducer messageProducer
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _jwt = jwt;
        _messageProducer = messageProducer;
    }

    public async Task<Guid> Handle(CreateClientLoanApplicationInfoRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<LoanApplication>(request.ClientLoanApplicationRequest);
        var client = await _loansSystem.Clients.GetById(new Guid(_jwt.UserId));

        result.ClientId = client.Id;
        result.FullName = $"{client.FirstName} {client.LastName}";

        await _loansSystem.LoanApplications.Add(result);
        await _loansSystem.CompleteAsync();

        _messageProducer.SendMessage(request);

        return result.Id;
    }
}