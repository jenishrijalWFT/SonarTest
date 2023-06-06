// using System.Collections;
// using AutoMapper;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.PurchaseBills;
// using Boss.Gateway.Domain.Entities;
// using FluentAssertions;
// using Moq;

// namespace Boss.Gateway.Application.UnitTests.Features.PurchaseBills
// {
//     public class GetPurchaseBillListByIdHandlerTests
//     {
//         private readonly Mock<IPurchaseBillRepository> _purchaseBillRepositoryMock;
//         private readonly Mock<IMapper> _mapperMock;
//         private readonly GetPurchaseBillListByIdHandler _handler;

//         public GetPurchaseBillListByIdHandlerTests()
//         {
//             _purchaseBillRepositoryMock = new Mock<IPurchaseBillRepository>();
//             _mapperMock = new Mock<IMapper>();
//             _handler = new GetPurchaseBillListByIdHandler(_purchaseBillRepositoryMock.Object, _mapperMock.Object);
//         }

//         [Theory]
//         [ClassData(typeof(PurchaseBillTestData))]
//         public async Task Handle_ValidPurchaseBillId_ReturnsList(PurchaseBill purchaseBill)
//         {
//             // Arrange
//             var query = new GetPurchaseBillByIdQuery { PurchaseBillId = purchaseBill.Id };
//             var expectedPurchaseBills = new List<PurchaseBill> {new PurchaseBill
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

//                     Transactions = new List<PurchaseBillTransaction>
//                     {
//                         new PurchaseBillTransaction
//                         {
//                             Id = Guid.NewGuid(),
//                             TransactionNumber = "NEPSE0001",
//                             CompanyName = "Stock A",
//                             Quantity = 100,
//                             Rate = 10,
//                             CommissionRate = 100.00m,
//                             SeboCommision=2.0m,
//                             EffRate=9.1m,
//                             CoQuantity=23,
//                             CoAmount=20,
//                         }
//                     }}};
//             _purchaseBillRepositoryMock.Setup(x => x.GetPurchaseBillById(purchaseBill.Id))
//                 .ReturnsAsync(expectedPurchaseBills);
//             _mapperMock.Setup(x => x.Map<List<PurchaseBill>>(expectedPurchaseBills))
//                 .Returns(expectedPurchaseBills);

//             // Act
//             var actualPurchaseBills = await _handler.Handle(query, CancellationToken.None);

//             // Assert
//             actualPurchaseBills.Should().NotBeNull();
//             actualPurchaseBills.Should().HaveCount(1);
//             actualPurchaseBills.Should().BeEquivalentTo(expectedPurchaseBills);
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
//             actualPurchaseBills.First().Transactions.Should().NotBeNull();
//             actualPurchaseBills.First().Transactions.Count.Should().Be(expectedPurchaseBills.First().Transactions.Count);
//             actualPurchaseBills.First().Transactions.First().Id.Should().Be(expectedPurchaseBills.First().Transactions.First().Id);
//             actualPurchaseBills.First().Transactions.First().TransactionNumber.Should().Be(expectedPurchaseBills.First().Transactions.First().TransactionNumber);
//             actualPurchaseBills.First().Transactions.First().CompanyName.Should().Be(expectedPurchaseBills.First().Transactions.First().CompanyName);
//             actualPurchaseBills.First().Transactions.First().Quantity.Should().Be(expectedPurchaseBills.First().Transactions.First().Quantity);
//             actualPurchaseBills.First().Transactions.First().Rate.Should().Be(expectedPurchaseBills.First().Transactions.First().Rate);
//             actualPurchaseBills.First().Transactions.First().CommissionRate.Should().Be(expectedPurchaseBills.First().Transactions.First().CommissionRate);
//             _purchaseBillRepositoryMock.Verify(x => x.GetPurchaseBillById(purchaseBill.Id), Times.Once);
//         }
//     }

//     public class PurchaseBillTestData : IEnumerable<object[]>
//     {
//         public IEnumerator<object[]> GetEnumerator()
//         {
//             yield return new object[] { new PurchaseBill { Id = Guid.NewGuid() } };
//             yield return new object[] { new PurchaseBill { Id = Guid.NewGuid() } };
//             yield return new object[] { new PurchaseBill { Id = Guid.NewGuid() } };
//         }

//         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//     }
// }