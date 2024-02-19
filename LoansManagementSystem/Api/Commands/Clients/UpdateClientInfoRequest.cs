using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.Clients;

public class UpdateClientInfoRequest : IRequest<bool>
{
    public UpdateClientRequest ClientRequest { get; }

    public UpdateClientInfoRequest(UpdateClientRequest clientRequest)
    {
        ClientRequest = clientRequest;
    }
}