using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.AccountHeads;
using Boss.Gateway.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Boss.Gateway.UnitTests.Application.Features.AccountHeads;

public class CreateAccountHeadCommandHandlerTests
{
    private readonly Mock<IAccountHeadRepository> _mockAccountHeadRepository;
    private readonly Mock<IMapper> _mapperMock;
    private CreateAccountHeadCommandHandler? _handler;

    public CreateAccountHeadCommandHandlerTests()
    {
        _mockAccountHeadRepository = new Mock<IAccountHeadRepository>();
        _mapperMock = new Mock<IMapper>();
        // _handler = new CreateAccountHeadCommandHandler(_mockAccountHeadRepository.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldNotAddAccountHeads()
    {
        //Arrange
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("InvalidFile.txt");


        var command = new CreateAccountHeadCommand
        {
            file = fileMock.Object
        };

        //Act
        var result = await _handler!.Handle(command, CancellationToken.None);
        //Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ValidationErrors);
        Assert.Equal(1, result.ValidationErrors.Count);
        _mockAccountHeadRepository.Verify(x => x.AddAccountHead(It.IsAny<List<AccountHead>>()), Times.Never);

    }

}