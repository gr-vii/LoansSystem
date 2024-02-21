using AutoMapper;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LoansManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
    protected readonly ILoansSystem _loansSystem;
    protected readonly IMapper _mapper;
    protected readonly Configurations _config;
    protected readonly IMediator _mediator;

    public BaseController(ILoansSystem loansSystem,
        IMapper mapper,
        IOptions<Configurations> config,
        IMediator mediator
    )
    {
        _loansSystem = loansSystem;
        _mapper = mapper;
        _config = config.Value;
        _mediator = mediator;
    }
}