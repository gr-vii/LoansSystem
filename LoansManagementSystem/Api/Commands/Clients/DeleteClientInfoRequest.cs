using MediatR;

namespace LoansManagementSystem.Api.Commands.Clients;

public class DeleteClientInfoRequest : IRequest<bool>
{
    public Guid ClientId { get; }

    public DeleteClientInfoRequest(Guid clientId)
    {
        ClientId = clientId;
    }
}