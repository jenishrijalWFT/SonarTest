using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.AccountHeads;
using Boss.Gateway.Domain.Entities;
using Moq;

namespace Boss.Gateway.UnitTests.Application.Features.AccountHeads;

public class GetAccountHeadListQueryTests
{
    private readonly Mock<IAccountHeadRepository> _mockAccountHeadRepository;
    private readonly GetAccountHeadListQueryHandler _handler;

    public GetAccountHeadListQueryTests()
    {
        _mockAccountHeadRepository = new Mock<IAccountHeadRepository>();
        _handler = new GetAccountHeadListQueryHandler(_mockAccountHeadRepository.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsAccountHeadList()
    {
        // Arrange
        var accountCodes = new HashSet<string> { "code1", "code2", "code3" };
        //_mockAccountHeadRepository.Setup(repo => repo.GetAccountHeadList()).ReturnsAsync(accountCodes);

        var request = new GetAccountHeadListQuery();
        var cancellationToken = new CancellationToken();

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.True(result is List<AccountHead>);
        Assert.Equal(accountCodes.Count, result.Count);

        for (int i = 0; i < accountCodes.Count; i++)
        {
            Assert.Equal(accountCodes.ElementAt(i), result[i].ClientCode);
        }

        _mockAccountHeadRepository.Verify(repo => repo.GetAccountHeadList(), Times.Once);
    }
}
