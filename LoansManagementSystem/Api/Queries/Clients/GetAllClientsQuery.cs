using LoansManagementSystem.Entities.Dtos.Responses;
using MediatR;

namespace LoansManagementSystem.Api.Queries.Clients;

public class GetAllClientsQuery : IRequest<IEnumerable<ClientResponse>> { }