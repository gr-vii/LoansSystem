using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Commands.Clients;

public class CreateClientInfoRequest : IRequest<ClientResponse>
{
    public CreateClientRequest ClientRequest { get; }

    public CreateClientInfoRequest(CreateClientRequest clientRequest)
    {
        ClientRequest = clientRequest;
    }
}