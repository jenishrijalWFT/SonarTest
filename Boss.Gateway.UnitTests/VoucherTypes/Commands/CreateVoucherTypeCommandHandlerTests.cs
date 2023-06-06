using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.VoucherTypes;
using Boss.Gateway.Domain.Entities;
using Moq;

namespace Boss.Gateway.Application.Tests.Features.VoucherTypes
{
    public class CreateVoucherTypeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreateVoucherTypeCommand
            {
                TypeName = "Test Voucher"
            };

            var voucherTypeRepositoryMock = new Mock<IVoucherTypeRepository>();
            voucherTypeRepositoryMock.Setup(repo => repo.AddVoucherTypeName(It.IsAny<VoucherType>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateVoucherTypeCommandHandler(
                voucherTypeRepositoryMock.Object
            );

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success);
            Assert.Equal($"Voucher Type {command.TypeName} Created Successfully", response.Message);
            voucherTypeRepositoryMock.Verify(repo => repo.AddVoucherTypeName(It.IsAny<VoucherType>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ShouldReturnValidationErrors()
        {
            // Arrange
            var voucherTypeRepositoryMock = new Mock<IVoucherTypeRepository>();
            var command = new CreateVoucherTypeCommand(); // Empty TypeName, which is invalid
            var validator = new CreateVoucherTypeCommandValidator();
            var cancellationToken = CancellationToken.None;

            var handler = new CreateVoucherTypeCommandHandler(voucherTypeRepositoryMock.Object);

            // Act
            var response = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.NotEmpty(response.ValidationErrors!);
            voucherTypeRepositoryMock.Verify(x => x.AddVoucherTypeName(It.IsAny<VoucherType>()), Times.Never);
        }
    }
}
