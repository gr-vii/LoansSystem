using AutoMapper;
using LoansManagementSystem.Api.Commands.Account;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Handlers.Account;

public class SignAdministratorInHandler : IRequestHandler<SignUserInRequest, string>
{
    private readonly ILoansSystem _loansSystem;
    private readonly IMapper _mapper;
    private readonly Configurations _config;
    private readonly IMemoryCache _cache;

    public SignAdministratorInHandler(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IMemoryCache cache
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
        _cache = cache;
    }

    public async Task<string> Handle(SignUserInRequest request, CancellationToken cancellationToken)
    {
        var client = await _loansSystem.Administrators.GetAdministratorByPhoneNumberAsync(request.UserRequest.PhoneNumber);

        if (client == null)
            return string.Empty;

        Hasher.CheckHashedPassword(request.UserRequest.Password, client.Salt, out var hashedPassword);

        if (client.Password != hashedPassword)
        {
            client.FailedLoginCounter += 1;
            client.LupDate = DateTime.UtcNow;

            if (client.FailedLoginCounter < _config.MaxFailedLogins)
                return string.Empty;

            client.Blocked = true;
            client.LupDate = DateTime.UtcNow;

            await _loansSystem.Administrators.Update(client);
            await _loansSystem.CompleteAsync();

            return string.Empty;
        }

        await _loansSystem.Administrators.Update(client);
        await _loansSystem.CompleteAsync();

        return JwtHelper.GetJwt(_cache.Get<string>(Configurations.CacheKeyUsersTsk),
            DateTime.Now.AddMinutes(_cache.Get<int>(Configurations.CacheKeyUsersJwtExpiration)),
            JwtHelper.SetClaims(client.Id, "Administrator"));
    }
}