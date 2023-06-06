// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.SellBills;
// using Boss.Gateway.Domain.Entities;
// using Moq;
// using AutoMapper;
// using System.Collections;
// using FluentAssertions;

// namespace Boss.Gateway.UnitTests.SellBillTest
// {
//     public class SellBillIdTestData : IEnumerable<object[]>
//     {
//         public IEnumerator<object[]> GetEnumerator()
//         {
//             yield return new object[] { new SellBill { Id = Guid.NewGuid() } };
//             yield return new object[] { new SellBill { Id = Guid.NewGuid() } };
//         }

//         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//     }

//     public class GetSellBillByIdQueryHandlerTest
//     {
//         private readonly Mock<IsellBillRepository> _mockSellBillRepository;
//         private readonly Mock<IMapper> _mockMapper;
//         private readonly GetSellBillListByIdHandler _handler;

//         public GetSellBillByIdQueryHandlerTest()
//         {
//             _mockSellBillRepository = new Mock<IsellBillRepository>();
//             _mockMapper = new Mock<IMapper>();
//             _handler = new GetSellBillListByIdHandler(_mockSellBillRepository.Object, _mockMapper.Object);
//         }

//         [Theory]
//         [ClassData(typeof(SellBillIdTestData))]
//         public async Task GetSellBillByIdQueryHandler_ShouldReturnSellBillListById(SellBill sellBill)
//         {
//             // Arrange
//             var query = new GetSellBillListByIdQuery { SellBillId = sellBill.Id };
//             var sellBilltransaction = new List<SellBillTransaction>();
//             var data = new List<SellBill> { sellBill };
//             _mockSellBillRepository.Setup(g => g.GetSellBillById(sellBill.Id)).ReturnsAsync(data);
//             _mockMapper.Setup(m => m.Map<List<SellBill>>(data)).Returns(data);

//             // Act
//             var result = await _handler.Handle(query, CancellationToken.None);

//             // Assert
//             result.Should().BeEquivalentTo(data);
//             result.Should().NotBeNull();
//             result.Should().HaveCount(1);
//             sellBilltransaction.Should().NotBeNull();
//             _mockSellBillRepository.Verify(x => x.GetSellBillById(sellBill.Id));
//         }
//     }
// }