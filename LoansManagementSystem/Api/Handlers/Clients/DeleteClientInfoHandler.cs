using AutoMapper;
using LoansManagementSystem.Api.Commands.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Clients;

public class DeleteClientInfoHandler : IRequestHandler<DeleteClientInfoRequest, bool>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public DeleteClientInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<bool> Handle(DeleteClientInfoRequest request, CancellationToken cancellationToken)
    {
        var Client = await _loansSystem.Clients.GetById(request.ClientId);

        if (Client == null)
            return false;

        await _loansSystem.Clients.Delete(request.ClientId);
        await _loansSystem.CompleteAsync();

        return true;
    }
}