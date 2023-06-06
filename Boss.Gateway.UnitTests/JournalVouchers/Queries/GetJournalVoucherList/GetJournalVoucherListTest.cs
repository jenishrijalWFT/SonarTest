// using AutoMapper;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.JournalVouchers;
// using Boss.Gateway.Domain.Entities;
// using Moq;

// namespace Boss.Gateway.Application.UnitTests.Features.Companies
// {
//     public class GetJournalVoucherListQueryHandlerTests
//     {
//         private readonly Mock<IJournalVoucherRepository> _mockJournalVoucherRepository;
//         private readonly Mock<IMapper> _mapperMock;

//         public GetJournalVoucherListQueryHandlerTests()
//         {
//             _mockJournalVoucherRepository = new Mock<IJournalVoucherRepository>();
//             _mapperMock = new Mock<IMapper>();
//         }

//         [Fact]
//         public async Task Handle_ShouldReturnJournalVoucherList()
//         {
//             // Arrange
//             var expectedJournalVoucher = new List<JournalVoucher>
//             {
//                 new JournalVoucher
//                     {
//                         Id = Guid.NewGuid(),
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
//                         JVTransactions = new List<JVTransaction>()
//                     },
//                 new JournalVoucher
//                     {
//                         Id = Guid.NewGuid(),
//                         ClientCode = "COD001",
//                         ClientName = "Client 1",
//                         VoucherDateAD = "2023-05-12",
//                         VoucherDateBS = "2079-01-28",
//                         VoucherNo = "JV/1001/2021",
//                         ReferenceNo = "b/1001/2021",
//                         Amount = 100.50M,
//                         ApprovedStatus = "Approved",
//                         CreatedAt = DateTime.Now,
//                         TypeId = Guid.NewGuid(),
//                         JVTransactions = new List<JVTransaction>()
//                     }

//             };

//             var query = new GetJournalVoucherListQuery { page = 1, size = 10 };
//             var queryHandler = new GetJournalVoucherListQueryHandler(_mockJournalVoucherRepository.Object, _mapperMock.Object);
//             _mockJournalVoucherRepository.Setup(x => x.GetJournalVouchersList(query)).ReturnsAsync(expectedJournalVoucher);
//             _mapperMock.Setup(mapper => mapper.Map<List<JournalVoucher>>(It.IsAny<List<JournalVoucher>>())).Returns(expectedJournalVoucher);

//             // Act
//             var actualJournalVouchers = await queryHandler.Handle(query, CancellationToken.None);

//             // Assert
//             Assert.NotNull(actualJournalVouchers);
//             Assert.Equal(expectedJournalVoucher.Count, actualJournalVouchers.Count);
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
//         }
//     }
// }
