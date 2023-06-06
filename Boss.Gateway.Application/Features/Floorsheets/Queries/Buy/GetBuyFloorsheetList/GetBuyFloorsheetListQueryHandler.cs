using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetBuyFloorsheetListQueryHandler : IRequestHandler<GetBuyFloorsheetListQuery, PaginatedResult<BuyFloorsheet>>


    {

        private readonly IFloorsheetRepository _floorsheetRepository;

        private readonly IRedisRepository _redisRepository;
        private readonly IMapper _mapper;



        public GetBuyFloorsheetListQueryHandler(IMapper mapper, IFloorsheetRepository floorsheetRepository, IRedisRepository redisRepository)
        {
            _floorsheetRepository = floorsheetRepository;
            _mapper = mapper;
            _redisRepository = redisRepository;
        }
        public async Task<PaginatedResult<BuyFloorsheet>> Handle(GetBuyFloorsheetListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction
                ("get-buy-floorsheet-list", "http-request");

                var (totalCount, pageSize, totalPages, currentPage, floorsheets) = await _floorsheetRepository.GetBuyFloorsheets(request);
                return new PaginatedResult<BuyFloorsheet>
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageSize = pageSize,
                    CurrentPage = currentPage,
                    Data = floorsheets,
                };

            }
            catch (Exception ex)
            {
                var customException = new Exception(
                 message: "Get Buy Floorsheet List Query  Failed", ex
             );
                customException.AddSentryTag("Get Buy Floorsheet Query Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }

        }
    }
}