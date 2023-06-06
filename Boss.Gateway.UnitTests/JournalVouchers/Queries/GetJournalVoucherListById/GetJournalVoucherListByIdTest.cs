// using AutoMapper;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.JournalVouchers;
// using Boss.Gateway.Domain.Entities;
// using Moq;

// namespace Boss.Gateway.Test.Queries
// {
//     public class GetJournalVoucherByIdQueryHandlerTests
//     {
//         private readonly Mock<IJournalVoucherRepository> _journalVoucherRepoMock;
//         private readonly Mock<IRedisRepository> _redisRepoMock;
//         private readonly Mock<IMapper> _mapperMock;

//         public GetJournalVoucherByIdQueryHandlerTests()
//         {
//             _journalVoucherRepoMock = new Mock<IJournalVoucherRepository>();
//             _redisRepoMock = new Mock<IRedisRepository>();
//             _mapperMock = new Mock<IMapper>();
//         }

//         [Fact]
//         // [ClassData(typeof(GetJournalVoucherByIdQueryHandlerTestData))]
//         public async Task Handle_Should_Return_Expected_JournalVoucher_List()
//         {
//             // Arrange
//             var expectedJournalVoucher = new List<JournalVoucher>
//             {
//                 new JournalVoucher
//                     {
//                         Id = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"),
//                         ClientCode = "jhfo7rcx8y",
//                         ClientName = "Client 1",
//                         VoucherDateAD = "2023-05-12",
//                         VoucherDateBS = "2079-01-28",
//                         VoucherNo = "JV001",
//                         ReferenceNo = "REF001",
//                         Amount = 100.50M,
//                         ApprovedStatus = "Approved",
//                         CreatedAt = DateTime.Now,
//                         TypeId = Guid.NewGuid(),
//                         JVTransactions = new List<JVTransaction>
//                         {
//                             new JVTransaction
//                             {
//                                 Id = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"),
//                                 Description = "Transaction Description",
//                                 Debit = 0.230m,
//                                 Credit = 0.234m,
//                                 JournalVoucherId = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"),
//                                 AccountHeadId = "123abc",
//                                 BranchId = Guid.NewGuid()
//                             }
//                         }
//                     }
//                     };
//             _journalVoucherRepoMock.Setup(repo => repo.GetJournalVoucherById(Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"))).ReturnsAsync(new JournalVoucher());
//             _mapperMock.Setup(mapper => mapper.Map<List<JournalVoucher>>(It.IsAny<List<JournalVoucher>>())).Returns(expectedJournalVoucher);
//             var query = new GetJournalVoucherByIdQuery { JournalVoucherId = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e") };
//             var queryHandler = new GetJournalVoucherByIdQueryHandler(_journalVoucherRepoMock.Object, _redisRepoMock.Object, _mapperMock.Object);

//             // Act
//             var actualJournalVouchers = await queryHandler.Handle(query, CancellationToken.None);

//             // Assert
//             Assert.NotNull(actualJournalVouchers);
//             Assert.Equal(expectedJournalVoucher.Count, actualJournalVouchers.Count);
//             _journalVoucherRepoMock.Verify(repo => repo.GetJournalVoucherById(Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e")), Times.Once());
//             Assert.Equal(expectedJournalVoucher.First().Id, actualJournalVouchers.First().Id);
//             Assert.Equal(expectedJournalVoucher.First().ClientName, actualJournalVouchers.First().ClientName);
//             Assert.Equal(expectedJournalVoucher.First().VoucherDateAD, actualJournalVouchers.First().VoucherDateAD);
//             Assert.Equal(expectedJournalVoucher.First().VoucherDateBS, actualJournalVouchers.First().VoucherDateBS);
//             Assert.Equal(expectedJournalVoucher.First().VoucherNo, actualJournalVouchers.First().VoucherNo);
//             Assert.Equal(expectedJournalVoucher.First().ReferenceNo, actualJournalVouchers.First().ReferenceNo);
//             Assert.Equal(expectedJournalVoucher.First().Amount, actualJournalVouchers.First().Amount);
//             Assert.Equal(expectedJournalVoucher.First().ApprovedStatus, actualJournalVouchers.First().ApprovedStatus);
//             Assert.Equal(expectedJournalVoucher.First().CreatedAt, actualJournalVouchers.First().CreatedAt);
//             Assert.Equal(expectedJournalVoucher.First().TypeId, actualJournalVouchers.First().TypeId);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions, actualJournalVouchers.First().JVTransactions);
//             Assert.NotNull(actualJournalVouchers.First().JVTransactions);
//             Assert.Equal(actualJournalVouchers.First().JVTransactions.Count, expectedJournalVoucher.First().JVTransactions.Count);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().Id, actualJournalVouchers.First().JVTransactions.First().Id);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().Description, actualJournalVouchers.First().JVTransactions.First().Description);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().Debit, actualJournalVouchers.First().JVTransactions.First().Debit);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().Credit, actualJournalVouchers.First().JVTransactions.First().Credit);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().JournalVoucherId, actualJournalVouchers.First().JVTransactions.First().JournalVoucherId);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().AccountHeadId, actualJournalVouchers.First().JVTransactions.First().AccountHeadId);
//             Assert.Equal(expectedJournalVoucher.First().JVTransactions.First().BranchId, actualJournalVouchers.First().JVTransactions.First().BranchId);

//         }

//         // private class GetJournalVoucherByIdQueryHandlerTestData : IEnumerable<object[]>
//         // {
//         //     public IEnumerator<object[]> GetEnumerator()
//         //     {
//         //         yield return new object[] { Guid.NewGuid(), new List<JournalVoucher>() { new JournalVoucher() } };
//         //     }
//         //     IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//         // }
//     }
// }
