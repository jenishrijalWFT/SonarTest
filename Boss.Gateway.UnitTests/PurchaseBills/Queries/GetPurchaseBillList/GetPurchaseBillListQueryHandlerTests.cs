// using AutoMapper;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.PurchaseBills;
// using Boss.Gateway.Domain.Entities;
// using FluentAssertions;
// using Moq;

// namespace Boss.Gateway.Application.UnitTests.Features.PurchaseBills
// {
//     public class GetPurchaseBillListQueryHandlerTests
//     {
//         private readonly Mock<IPurchaseBillRepository> _mockPurchaseBillRepository;
//         private readonly Mock<IMapper> _mockMapper;

//         public GetPurchaseBillListQueryHandlerTests()
//         {
//             _mockPurchaseBillRepository = new Mock<IPurchaseBillRepository>();
//             _mockMapper = new Mock<IMapper>();
//         }

//         [Fact]
//         public async Task GetPurchaseBillListQueryHandler_ShouldReturnPurchaseBillList()
//         {
//             // Arrange
//             var expectedPurchaseBills = new List<PurchaseBill>
//             {
//                 new PurchaseBill
//                 {
//                     Id = Guid.NewGuid(),
//                     ClientCode = "1232372384",
//                     ClientName = "John Doe",
//                     BillNumber = "b/1001/2021",
//                     BillDate = "2023-05-11T09:45:04.526Z",
//                     NepseCommission = 2.0m,
//                     SeboCommission = 30.0m,
//                     SeboRegulatoryFee = 40,
//                     ClearanceDate = "2023-05-02",
//                     DpAmount = 50,

//                     Transactions = new List<PurchaseBillTransaction>()
//                 }
//             };
//             var query = new GetPurchaseBillListQuery { page = 1, size = 40 };
//             _mockPurchaseBillRepository.Setup(repo => repo.GetPurchaseBills(query)).ReturnsAsync(expectedPurchaseBills);

//             var mapperConfig = new MapperConfiguration(cfg =>
//           {
//               cfg.CreateMap<PurchaseBill, PurchaseBill>();
//               cfg.CreateMap<PurchaseBillTransaction, PurchaseBillTransaction>();
//           });
//             var mapper = mapperConfig.CreateMapper();
//             var handler = new GetPurchaseBillListQueryHandler(_mockPurchaseBillRepository.Object, mapper);

//             // Act
//             var actualPurchaseBills = await handler.Handle(query, CancellationToken.None);

//             // Assert
//             actualPurchaseBills.Should().NotBeNull();
//             actualPurchaseBills.Count.Should().Be(expectedPurchaseBills.Count);
//             actualPurchaseBills.First().Id.Should().Be(expectedPurchaseBills.First().Id);
//             actualPurchaseBills.First().ClientCode.Should().Be(expectedPurchaseBills.First().ClientCode);
//             actualPurchaseBills.First().ClientName.Should().Be(expectedPurchaseBills.First().ClientName);
//             actualPurchaseBills.First().BillNumber.Should().Be(expectedPurchaseBills.First().BillNumber);
//             actualPurchaseBills.First().BillDate.Should().Be(expectedPurchaseBills.First().BillDate);
//             actualPurchaseBills.First().NepseCommission.Should().Be(expectedPurchaseBills.First().NepseCommission);
//             actualPurchaseBills.First().SeboCommission.Should().Be(expectedPurchaseBills.First().SeboCommission);
//             actualPurchaseBills.First().SeboRegulatoryFee.Should().Be(expectedPurchaseBills.First().SeboRegulatoryFee);
//             actualPurchaseBills.First().ClearanceDate.Should().Be(expectedPurchaseBills.First().ClearanceDate);
//             actualPurchaseBills.First().DpAmount.Should().Be(expectedPurchaseBills.First().DpAmount);
//             _mockPurchaseBillRepository.Verify(repo => repo.GetPurchaseBills(query), Times.Once);
//         }
//     }
// }
