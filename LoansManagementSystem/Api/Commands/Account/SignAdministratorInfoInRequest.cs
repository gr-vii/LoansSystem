using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.Account;

public class SignAdministratorInfoInRequest : IRequest<string>
{
    public SignAdministratorInRequest UserRequest { get; }

    public SignAdministratorInfoInRequest(SignAdministratorInRequest userRequest)
    {
        UserRequest = userRequest;
    }
}