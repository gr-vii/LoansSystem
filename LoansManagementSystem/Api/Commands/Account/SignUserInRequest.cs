using LoansManagementSystem.Entities.Dtos.Requests;
using MediatR;

namespace LoansManagementSystem.Api.Commands.Account;

public class SignUserInRequest : IRequest<string>
{
    public SignInRequest UserRequest { get; }

    public SignUserInRequest(SignInRequest userRequest)
    {
        UserRequest = userRequest;
    }
}