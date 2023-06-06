using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.JournalVouchers.Queries;
using Boss.Gateway.Domain.Entities;
using Moq;

namespace Boss.Gateway.UnitTests.JournalVouchers.Queries
{
    public class GetUnbilledTransactionJournalVoucherQueryHandlerTests
    {
        private readonly Mock<IJournalVoucherRepository> _mockRepo;
        private readonly GetUnbilledTransactionJournalVoucherQueryHandler _handler;

        public GetUnbilledTransactionJournalVoucherQueryHandlerTests()
        {
            _mockRepo = new Mock<IJournalVoucherRepository>();
            _handler = new GetUnbilledTransactionJournalVoucherQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturn_UnbilledTransaction()
        {
            // Arrange
            var clientCode = "test";
            var expected = new List<JournalVoucher>
            {
                new JournalVoucher
                {
                    Id = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"),
                    ClientName = "test",
                    ClientCode = "test",
                    VoucherDateAD = "test",
                    VoucherDateBS = "test",
                    VoucherNo = "test",
                    ReferenceNo = "test",
                    Amount = 125555m,
                    ApprovedStatus = "test",
                    CreatedAt = DateTime.Parse("2023-05-15 16:11:08")
                }
            };

            _mockRepo.Setup(x => x.GetUnbilledTransactionJournalVouchers(It.IsAny<GetUnbilledTransactionJournalVoucherQuery>()))
                .ReturnsAsync((GetUnbilledTransactionJournalVoucherQuery query) =>
                {
                    return expected.Where(jv => jv.ClientCode == query.ClientCode).ToList();
                });

            var query = new GetUnbilledTransactionJournalVoucherQuery
            {
                ClientCode = clientCode
            };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected.First().Id, result.First().Id);
            Assert.Equal(expected.First().ClientCode, result.First().ClientCode);
        }
    }
}
