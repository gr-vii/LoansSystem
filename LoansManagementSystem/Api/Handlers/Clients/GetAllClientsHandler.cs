using AutoMapper;
using LoansManagementSystem.Api.Queries.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Clients;

public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientResponse>>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public GetAllClientsHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<IEnumerable<ClientResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var Client = await _loansSystem.Clients.All();
        return _mapper.Map<IEnumerable<ClientResponse>>(Client);
    }
}