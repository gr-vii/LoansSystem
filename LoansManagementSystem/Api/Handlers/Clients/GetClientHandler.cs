using AutoMapper;
using LoansManagementSystem.Api.Queries.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Clients;

public class GetClientHandler : IRequestHandler<GetClientQuery, ClientResponse>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public GetClientHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<ClientResponse> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var Client = await _loansSystem.Clients.GetById(request.ClientId);

        return _mapper.Map<ClientResponse>(Client);
    }
}