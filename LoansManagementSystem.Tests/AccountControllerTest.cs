using AutoMapper;
using LoansManagementSystem.Api.Commands.Account;
using LoansManagementSystem.Api.Controllers;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LoansManagementSystem.Tests;

public class AccountControllerTests
{
    private readonly Mock<ILoansSystem> _loansSystemMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IOptions<Configurations>> _configMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _loansSystemMock = new Mock<ILoansSystem>();
        _mapperMock = new Mock<IMapper>();
        _configMock = new Mock<IOptions<Configurations>>();
        _mediatorMock = new Mock<IMediator>();
        _controller = new AccountController(_loansSystemMock.Object, _mapperMock.Object, _configMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task SignClientIn_ReturnsUnauthorized()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "96427736030362", Password = "Abcd1234" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<SignUserInRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(String.Empty);

        // Act
        var result = await _controller.SignClientIn(signInRequest);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task SignClientIn_ReturnsBadRequest()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "96427736030362", Password = "Abcd1234" };
        _controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await _controller.SignClientIn(signInRequest);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task SignClientIn_ReturnsOk()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "96427736030362", Password = "Abcd1234" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<SignUserInRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxOTU5MTI2ZC0zYTgwLTQ3MmItODhmMy0wNDgzZDIyZjQxOTAiLCJVc2VyUm9sZSI6IkFkbWluaXN0cmF0b3IiLCJleHAiOjE3MDgzODg0MDMsImlzcyI6IkxNUyIsImF1ZCI6IkxvYW5zTWFuYWdlbWVudFN5c3RlbSJ9.IwkD08TipuOU3jN8K7D56h5UzYC4OP8fybjJmliNjpo");

        // Act
        var result = await _controller.SignClientIn(signInRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task SignAdminIn_ReturnsUnauthorized()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "96427736030362", Password = "Abcd1234" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<SignUserInRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(string.Empty);

        // Act
        var result = await _controller.SignAdminIn(signInRequest);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task SignAdminIn_ReturnsBadRequest()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "InvalidPhoneNumber", Password = "Abcd1234" };
        _controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await _controller.SignClientIn(signInRequest);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task SignAdminIn_ReturnsOk()
    {
        // Arrange
        var signInRequest = new SignInRequest { PhoneNumber = "96427736030362", Password = "Abcd1234" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<SignUserInRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxOTU5MTI2ZC0zYTgwLTQ3MmItODhmMy0wNDgzZDIyZjQxOTAiLCJVc2VyUm9sZSI6IkFkbWluaXN0cmF0b3IiLCJleHAiOjE3MDgzODg0MDMsImlzcyI6IkxNUyIsImF1ZCI6IkxvYW5zTWFuYWdlbWVudFN5c3RlbSJ9.IwkD08TipuOU3jN8K7D56h5UzYC4OP8fybjJmliNjpo");

        // Act
        var result = await _controller.SignAdminIn(signInRequest);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}