using AutoMapper;
using LoansManagementSystem.Api.Commands.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Clients;

public class AddClientInfoHandler : IRequestHandler<CreateClientInfoRequest, ClientResponse>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public AddClientInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<ClientResponse> Handle(CreateClientInfoRequest request, CancellationToken cancellationToken)
    {
        var client = _mapper.Map<Client>(request.ClientRequest);

        Hasher.GetHashedPassword(client.Password, out var hashedPassword, out var salt);

        client.Password = hashedPassword;
        client.Salt = salt;

        await _loansSystem.Clients.Add(client);
        await _loansSystem.CompleteAsync();

        return _mapper.Map<ClientResponse>(client);
    }
}