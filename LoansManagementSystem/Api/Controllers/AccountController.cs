using AutoMapper;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Security;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    public AccountController(ILoansSystem loansSystem, IMapper mapper, IOptions<Configurations> config, IMediator mediator) : base(loansSystem, mapper, config, mediator) { }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> SignClientIn([FromBody] CreateAccountRequest account)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _loansSystem.Clients.GetClientByPhoneNumberAsync(account.PhoneNumber);

        if (client == null)
            return NotFound("User not found");

        Hasher.CheckHashedPassword(client.Password, client.Salt, out var hashedPassword);

        //if (client.Password != hashedPassword)
        //{
        //    client.FailedLoginCounter += 1;
        //    client.LupDate = DateTime.UtcNow;

        //    if (client.FailedLoginCounter >= _config.MaxFailedLogins)
        //    {
        //        client.Blocked = true;
        //        client.LupDate = DateTime.UtcNow;

        //        _db.T_USERS.Update(user);

        //        await _db.SaveChangesAsync();

        //        return this.Response("You have exceeded your maximum login tries, your user is blocked", null, StatusCodes.Status403Forbidden);
        //    }

        //    return this.Response("Invalid username or password", null, StatusCodes.Status403Forbidden);
        //}

        //db.T_USERS.Update(user);

        //await db.SaveChangesAsync();

        //await _loansSystem.CompleteAsync();

        return NoContent();
    }
}