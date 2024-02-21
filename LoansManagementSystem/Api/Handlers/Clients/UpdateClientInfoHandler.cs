using AutoMapper;
using LoansManagementSystem.Api.Commands.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Models;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Clients;

public class UpdateClientInfoHandler : IRequestHandler<UpdateClientInfoRequest, bool>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;

    public UpdateClientInfoHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
    }

    public async Task<bool> Handle(UpdateClientInfoRequest request, CancellationToken cancellationToken)
    {
        var result = _mapper.Map<Client>(request.ClientRequest);

        Hasher.GetHashedPassword(result.Password, out var hashedPassword, out var salt);

        result.Password = hashedPassword;
        result.Salt = salt;

        await _loansSystem.Clients.Update(result);
        await _loansSystem.CompleteAsync();

        return true;
    }
}