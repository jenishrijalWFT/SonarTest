// using Boss.Gateway.Domain.Entities;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.SellBills;
// using Moq;
// using AutoMapper;
// using FluentAssertions;


// namespace Boss.Gateway.Application.UnitTests.Features.SellBills
// {
//     public class GetSellBillQueryHandlerTest
//     {
//         private readonly Mock<IsellBillRepository> _mockSellBillRepository;
//         private readonly Mock<IMapper> _mockMapper;
//         public GetSellBillQueryHandlerTest()
//         {
//             _mockSellBillRepository = new Mock<IsellBillRepository>();
//             _mockMapper = new Mock<IMapper>();
//         }

//         [Fact]
//         public async Task GetSellBillListQueryHandler_ShouldReturnSellBillList()
//         {
//             //Arrange
//             var expectedSellBill = new List<SellBill>
//         {
//             new SellBill
//             {
//                 Id = Guid.NewGuid(),
//                 ClientCode = "1234567895",
//                 ClientName = "Lol",
//                 BillNumber = "S/1/80/81",
//                 BillDate = "2023-05-10",
//                 BrokerCommission = 100,
//                 NepseCommission = 50,
//                 SeboCommission = 20,
//                 SeboRegulatoryFee = 10,
//                 ClearanceDate = "2023-05-11",
//                 DpAmount = 50,
//                 CreatedAt = DateTime.Now,
//                 Transactions = new List<SellBillTransaction>()
//             }
//         };
//             var query = new GetSellBillListQuery { page = 1, size = 10 };
//             _mockSellBillRepository.Setup(r => r.GetsellBills(query)).ReturnsAsync(expectedSellBill);

//             var configMapper = new MapperConfiguration(cfg =>
//             {
//                 cfg.CreateMap<SellBill, SellBill>();
//             });

//             var mapper = configMapper.CreateMapper();
//             var handler = new GetSellBillListQueryHandler(_mockSellBillRepository.Object, mapper);

//             //Act
//             var actualSellBills = await handler.Handle(query, CancellationToken.None);

//             // Assert
//             actualSellBills.Should().BeEquivalentTo(expectedSellBill);
//             actualSellBills.First().ClientName.Should().Be(expectedSellBill.First().ClientName);
//             actualSellBills.First().BillNumber.Should().Be(expectedSellBill.First().BillNumber);
//             actualSellBills.First().BillDate.Should().Be(expectedSellBill.First().BillDate);
//             actualSellBills.First().ClientCode.Should().Be(expectedSellBill.First().ClientCode);
//             actualSellBills.First().BrokerCommission.Should().Be(expectedSellBill.First().BrokerCommission);
//             actualSellBills.First().NepseCommission.Should().Be(expectedSellBill.First().NepseCommission);
//             actualSellBills.First().SeboRegulatoryFee.Should().Be(expectedSellBill.First().SeboRegulatoryFee);
//             actualSellBills.First().ClearanceDate.Should().Be(expectedSellBill.First().ClearanceDate);
//             actualSellBills.First().DpAmount.Should().Be(expectedSellBill.First().DpAmount);
//             actualSellBills.First().Transactions.Should().NotBeNull();
//             _mockSellBillRepository.Verify(repo => repo.GetsellBills(query), Times.Once);
//         }
//     }
// }