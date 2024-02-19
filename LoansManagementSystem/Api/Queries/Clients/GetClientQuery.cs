using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Queries.Clients;

public class GetClientQuery : IRequest<ClientResponse>
{
    public Guid ClientId { get; }

    public GetClientQuery(Guid clientId)
    {
        ClientId = clientId;
    }
}