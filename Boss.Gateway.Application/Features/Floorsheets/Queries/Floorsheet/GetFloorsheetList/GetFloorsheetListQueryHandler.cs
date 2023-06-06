using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetFloorsheetListQueryHandler : IRequestHandler<GetFloorsheetListQuery, PaginatedResult<FloorsheetListVm>>


    {

        private readonly IFloorsheetRepository _floorsheetRepository;
        private readonly IMapper _mapper;

        public GetFloorsheetListQueryHandler(IMapper mapper, IFloorsheetRepository floorsheetRepository)
        {
            _floorsheetRepository = floorsheetRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<FloorsheetListVm>> Handle(GetFloorsheetListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-floorsheets-list", "http-request");

                var (totalCount, pageSize, totalPages, currentPage, floorsheets) = await _floorsheetRepository.GetFloorsheets(request);
                sentryTransaction.Finish();
                var dtos = new List<FloorsheetListVm>();
                foreach (var floorsheet in floorsheets)
                {
                    dtos.Add(new FloorsheetListVm()
                    {
                        Id = floorsheet.Id,
                        FiscalYear = floorsheet.FiscalYear,
                        FloorsheetDateAd = floorsheet.FloorsheetDateAd!,
                        FloorsheetDateBs = floorsheet.FloorsheetDateBs!,
                        ImportDateAd = floorsheet.ImportDateAd,
                        ImportDateBs = floorsheet.ImportDateBs,
                        FloorsheetAmount = floorsheet.Amount,
                        FloorsheetStockCommission = floorsheet.StockCommission,
                        FloorsheetBankDeposit = floorsheet.BankDeposit,
                        CreatedAt = floorsheet.CreatedAt
                    });
                }

                return new PaginatedResult<FloorsheetListVm>()
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageSize = pageSize,
                    Data = dtos,
                };
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                     message: "Get Floorsheet List Query  Failed", ex
                 );
                customException.AddSentryTag("Get Floorsheet List Query Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}