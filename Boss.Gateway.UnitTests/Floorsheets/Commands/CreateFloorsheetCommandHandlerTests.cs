// using AutoMapper;
// using Boss.Gateway.Application.Contracts.Persistence;
// using Boss.Gateway.Application.Features.Companies;
// using Boss.Gateway.Domain.Entities;
// using FluentAssertions;
// using MediatR;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.Internal;
// using Moq;

// namespace Boss.Gateway.Application.Features.FloorSheets.Tests
// {
//     public class CreateFloorSheetCommandHandlerTests
//     {
//         [Fact]
//         public async Task Handle_ValidFileFormat_ReturnsSuccessResponse()
//         {
//             // Arrange
//             var mockFloorSheetRepository = new Mock<IFloorsheetRepository>();
//             var mockMapper = new Mock<IMapper>();
//             var mockMediator = new Mock<IMediator>();

//             var handler = new CreateFloorSheetCommandHandler(
//                 mockFloorSheetRepository.Object,
//                 mockMapper.Object,
//                 mockMediator.Object
//             );

// ../../

// var filePath = @"../../../../Files/Floorsheet.xlsx";

//             using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//             {
//                 var fileName = Path.GetFileName(filePath);
//                 var formFile = new FormFile(fileStream, 0, fileStream.Length, "file", fileName);

//                 var command = new CreateFloorSheetCommand
//                 {
//                     file = formFile,
//                 };

//                 var companies = new List<Company>
//             {
//                 new Company
//                 {
//                     Symbol = "PCBL",
//                     Name = "ABC Company"
//                     // Set other properties as needed
//                 },
//                 new Company
//                 {
//                     Symbol = "LBL",
//                     Name = "XYZ Company"
//                     // Set other properties as needed
//                 },
//                 new Company
//                 {
//                     Symbol = "BARUN",
//                     Name = "XYZ Company"
//                     // Set other properties as needed
//                 },
//             };


//                 mockMediator.Setup(m => m.Send(It.IsAny<GetCompanyListQuery>(), It.IsAny<CancellationToken>()))
//                     .ReturnsAsync(companies);

//                 // Act
//                 var response = await handler.Handle(command, CancellationToken.None);

//                 // Assert
//                 response.Should().NotBeNull();
//                 response.Success.Should().BeTrue();
//                 response.Message.Should().Contain("Floorsheet uploaded successfully");

//                 mockFloorSheetRepository.Verify(
//                     r => r.FloorsheetEntry(
//                         It.IsAny<Floorsheet>(),
//                         It.IsAny<List<BuyFloorsheet>>(),
//                         It.IsAny<List<SellFloorsheet>>(),
//                         It.IsAny<List<Company>>()
//                     ),
//                     Times.Once
//                 );
//             }
//         }


//         [Fact]
//         public async Task Handle_InValidFileFormat_ReturnsExceptionError()
//         {
//             // Arrange
//             var mockFloorSheetRepository = new Mock<IFloorsheetRepository>();
//             var mockMapper = new Mock<IMapper>();
//             var mockMediator = new Mock<IMediator>();

//             var handler = new CreateFloorSheetCommandHandler(
//                 mockFloorSheetRepository.Object,
//                 mockMapper.Object,
//                 mockMediator.Object
//             );

//             var filePath = @"C:\Users\kabdu\intern\kajiman\Boss.Gateway.Version1\Boss.Gateway.UnitTest.ForFloorsheet\Boss.Gateway\Boss.Gateway.UnitTests\Files\Floorsheet.doc";

//             IFormFile floorsheetFile;

//             using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//             {
//                 var fileName = Path.GetFileName(filePath);
//                 var fileContent = new byte[fileStream.Length];
//                 fileStream.Read(fileContent, 0, (int)fileStream.Length);
//                 var formFile = new FormFile(new MemoryStream(fileContent), 0, fileContent.Length, "file", fileName);
//                 floorsheetFile = formFile;

//             }
//             var command = new CreateFloorSheetCommand
//             {
//                 file = floorsheetFile,
//             };

//             mockMapper.Setup(m => m.Map<Floorsheet>(It.IsAny<object>())).Returns(new Floorsheet());
//             var mockCompanies = new List<Company>
//             {
//                 new Company { Symbol = "PCBL" },
//                 new Company { Symbol = "LBL" },
//                 new Company { Symbol = "BARUN" }
//             };

//             // Configure the mock behavior of the Mediator to return the mockCompanies list when GetCompanyListQuery is executed
//             mockMediator.Setup(m => m.Send(It.IsAny<GetCompanyListQuery>(), It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(mockCompanies);

//             // Act
//             var response = await handler.Handle(command, CancellationToken.None);

//             // Assert
//             response.Should().NotBeNull();
//             response.Success.Should().BeFalse();
//             response.ValidationErrors.Should().Contain("File type must be .xls, .xlsx, or .csv");
//         }

//         // [Fact]
//         // public async Task Handle_InvalidCompanySymbol_ReturnsExceptionError()
//         // {
//         //     // Arrange
//         //     var mockFloorSheetRepository = new Mock<IFloorsheetRepository>();
//         //     var mockMapper = new Mock<IMapper>();
//         //     var mockMediator = new Mock<IMediator>();

//         //     var handler = new CreateFloorSheetCommandHandler(
//         //         mockFloorSheetRepository.Object,
//         //         mockMapper.Object,
//         //         mockMediator.Object
//         //     );

// var filePath = @"../../Files/Floorsheet.xlsx";

//         //     using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//         //     {
//         //         var fileName = Path.GetFileName(filePath);
//         //         var formFile = new FormFile(fileStream, 0, fileStream.Length, "file", fileName);

//         //         var command = new CreateFloorSheetCommand
//         //         {
//         //             file = formFile,
//         //         };

//         //         var companies = new List<Company>
//         //     {
//         //         new Company
//         //         {
//         //             Symbol = "PCBL",
//         //             Name = "ABC Company"
//         //             // Set other properties as needed
//         //         },
//         //         new Company
//         //         {
//         //             Symbol = "JCLBL",
//         //             Name = "XYZ Company"
//         //             // Set other properties as needed
//         //         },
//         //         new Company
//         //         {
//         //             Symbol = "BARUN",
//         //             Name = "XYZ Company"
//         //             // Set other properties as needed
//         //         },
//         //     };

//         //         mockMediator.Setup(m => m.Send(It.IsAny<GetCompanyListQuery>(), It.IsAny<CancellationToken>()))
//         //             .ReturnsAsync(companies);

//         //         // Act
//         //         Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

//         //         // Assert
//         //         var expectedMessage = $"LBL does not lie in the companies database! Please insert the LBL and 'company name' in the companies database";
//         //         await act.Should().ThrowAsync<Exception>().WithMessage(expectedMessage);
//         //     }
//     }
// }