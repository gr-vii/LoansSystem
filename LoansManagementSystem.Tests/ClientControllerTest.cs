using AutoMapper;
using LoansManagementSystem.Api.Commands.Clients;
using LoansManagementSystem.Api.Controllers;
using LoansManagementSystem.Api.Queries.Clients;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.Entities.Dtos.Requests;
using LoansManagementSystem.Entities.Dtos.Responses;
using LoansManagementSystem.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace LoansManagementSystem.Tests;

public class ClientsControllerTests
{
    private readonly Mock<ILoansSystem> _loansSystemMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IOptions<Configurations>> _configMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ClientsController _controller;

    public ClientsControllerTests()
    {
        _loansSystemMock = new Mock<ILoansSystem>();
        _mapperMock = new Mock<IMapper>();
        _configMock = new Mock<IOptions<Configurations>>();
        _mediatorMock = new Mock<IMediator>();
        _controller = new ClientsController(_loansSystemMock.Object, _mapperMock.Object, _configMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task GetClient_ReturnsOk()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetClientQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ClientResponse());

        // Act
        var result = await _controller.GetClient(clientId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddClient_ReturnsBadRequest()
    {
        // Arrange
        var createClientRequest = new CreateClientRequest { };
        _controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await _controller.AddClient(createClientRequest);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdateClient_ReturnsBadRequest()
    {
        // Arrange
        var updateClientRequest = new UpdateClientRequest { };
        _controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await _controller.UpdateClient(updateClientRequest);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetAllClients_ReturnsOk()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllClientsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<ClientResponse>());

        // Act
        var result = await _controller.GetAllClients();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteClient_ReturnsBadRequest()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteClientInfoRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteClient(clientId);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}