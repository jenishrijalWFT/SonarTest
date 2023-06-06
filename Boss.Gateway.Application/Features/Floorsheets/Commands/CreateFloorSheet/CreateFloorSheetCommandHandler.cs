using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.Companies;
using Boss.Gateway.Application.Infrastructure;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.FloorSheets
{

    public class CreateFloorSheetCommandHandler : IRequestHandler<CreateFloorSheetCommand, CreateFloorSheetCommandResponse>
    {
        private readonly IFloorsheetRepository _floorsheetRepository;
        private readonly IMapper _mapper;
        private readonly IAccountHeadRepository _accountHeadRepository;
        private readonly IMediator _mediator;

        private readonly ICsvHelper _csvHelper;

        public CreateFloorSheetCommandHandler(IFloorsheetRepository floorsheetRepository, IMapper mapper, IMediator mediator, IAccountHeadRepository accountHeadRepository, ICsvHelper csvHelper)
        {
            _floorsheetRepository = floorsheetRepository;
            _mapper = mapper;
            _mediator = mediator;
            _accountHeadRepository = accountHeadRepository;
            _csvHelper = csvHelper;
        }


        public async Task<CreateFloorSheetCommandResponse> Handle(CreateFloorSheetCommand request, CancellationToken cancellationToken)
        {
            var transaction = SentrySdk.StartTransaction("create-floorsheet", "http-request");
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("create-floorsheet", "http-request");
                var response = new CreateFloorSheetCommandResponse();
                var validator = new CreateFloorSheetCommandValidator();
                var validationResult = await validator.ValidateAsync(request);
                if (validationResult.Errors.Count > 0)
                {
                    response.Success = false;
                    response.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);

                    }
                    var customException = new Exception(
                        message: "Invalid file type upload attempt"
                    );

                    SentrySdk.CaptureException(customException);
                    customException.AddSentryTag("Floorsheet Upload", "Invalid file upload");
                    throw customException;
                }

                var companies = await _mediator.Send(new GetCompanyListQuery());
                var accountDetails = await _accountHeadRepository.GetAccountHeadList();
                var (floorsheetEntry, buyFloorSheets, sellFloorSheets) = _csvHelper.GetFloorsheetList(request.file!);
                var unmappedClientCodes = buyFloorSheets
                    .Where(b => !accountDetails.Select(a => a.clientCode).Contains(b.ClientCode))
                    .Select(b => b.ClientCode)
                    .Union(sellFloorSheets
                        .Where(s => !accountDetails.Select(a => a.clientCode).Contains(s.ClientCode))
                        .Select(s => s.ClientCode))
                    .ToList();
                //var mappedClientCodes = accountDetails.ToList().Except(unmappedClientCodes);
                var invalidSymbols = buyFloorSheets
                .Where(b => !companies.Any(c => c.Symbol == b.Symbol))
                .Select(b => b.Symbol)
                .Union(sellFloorSheets
                    .Where(s => !companies.Any(c => c.Symbol == s.Symbol))
                    .Select(s => s.Symbol))
                .ToList();
                if (invalidSymbols.Any())
                {
                    var invalidSymbolsString = string.Join(", ", invalidSymbols);
                    var errorMessage = $"Company symbols not found. Please add them to system first: {invalidSymbolsString}";
                    response.Success = false;
                    response.Message = errorMessage;
                    throw new Exception(response.Message);
                }
                if (response.Success)
                {
                    await _floorsheetRepository.FloorsheetEntry(floorsheetEntry, buyFloorSheets, sellFloorSheets, companies);
                    response.Message = "Floorsheet uploaded successfully";
                }
                return response;
            }
            catch (Exception ex)
            {

                var customException = new Exception(
                   message: "Floorsheet Creation Failed", ex
               );
                customException.AddSentryTag("Floorsheet", "failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);

            }
        }
    }
}

