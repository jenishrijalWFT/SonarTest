using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.Companies;
using Boss.Gateway.Domain.Entities;
using Moq;

namespace Boss.Gateway.UnitTests.Application.Features.Companies
{
    public class CreateCompanyCommandHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockRepository;

        public CreateCompanyCommandHandlerTests()
        {
            _mockRepository = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnSuccessResult()
        {
            // Arrange
            var command = new CreateCompanyCommand
            {
                Name = "Test Company",
                Symbol = "TEST",
                Email = "test@test.com",
                Website = "http://test.com",
                InstrumentType = "Equity"
            };

            var expectedCompany = new Company
            {
                Name = command.Name,
                Symbol = command.Symbol,
                Email = command.Email,
                Website = command.Website,
                InstrumentType = command.InstrumentType
            };

            _mockRepository.Setup(x => x.AddCompanyAsync(expectedCompany))
                .ReturnsAsync(expectedCompany);

            var handler = new CreateCompanyCommandHandler(_mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("New Company added successfully", result.Message);
            Assert.Null(result.ValidationErrors);
        }


        [Fact]
        public async Task Handle_InvalidRequest_ShouldReturnValidationErrorResult()
        {
            // Arrange
            var command = new CreateCompanyCommand
            {
                Name = "",
                Symbol = "",
                Email = "test@gmail.com",
                Website = "test.com",
                InstrumentType = ""
            };

            var handler = new CreateCompanyCommandHandler(_mockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Empty(result.Message);
            Assert.NotNull(result.ValidationErrors);
            _mockRepository.Verify(x => x.AddCompanyAsync(It.IsAny<Company>()), Times.Never);
        }
    }
}


