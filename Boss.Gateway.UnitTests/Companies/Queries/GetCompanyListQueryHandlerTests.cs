using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.Companies;
using Boss.Gateway.Domain.Entities;
using Moq;

namespace Boss.Gateway.Application.UnitTests.Features.Companies
{
    public class GetCompanyListQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly GetCompanyListQueryHandler _handler;

        public GetCompanyListQueryHandlerTests()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _handler = new GetCompanyListQueryHandler(_mockCompanyRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnCompanyList()
        {
            // Arrange
            var expectedCompanies = new List<Company>
            {
                new Company { Id = Guid.Parse("a27efc80-6dc5-4c34-9bb2-efc9f3d92e3e"), Name = "Company A" },
                new Company { Id = Guid.Parse("71b42716-06bb-46a2-9c7f-62c8f166b10d"), Name = "Company B" },
                new Company { Id = Guid.Parse("e11d3f3e-44f7-4f50-a9ad-4d4ec402e4db"), Name = "Company C" }
            };

            _mockCompanyRepository.Setup(x => x.GetCompanyList()).ReturnsAsync(expectedCompanies);

            var query = new GetCompanyListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCompanies.Count, result.Count);
            Assert.Equal(expectedCompanies.First().Id, result.First().Id);
            Assert.Equal(expectedCompanies.First().Name, result.First().Name);
            Assert.Equal(expectedCompanies.Last().Id, result.Last().Id);
            Assert.Equal(expectedCompanies.Last().Name, result.Last().Name);
        }
    }
}
