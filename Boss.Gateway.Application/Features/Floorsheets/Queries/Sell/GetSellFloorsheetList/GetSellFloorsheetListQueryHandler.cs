using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;


namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetSellFloorsheetListQueryHandler : IRequestHandler<GetSellFloorsheetListQuery, PaginatedResult<SellFloorsheet>>


    {

        private readonly IFloorsheetRepository _floorsheetRepository;

        private readonly IRedisRepository _redisRepository;
        private readonly IMapper _mapper;



        public GetSellFloorsheetListQueryHandler(IMapper mapper, IFloorsheetRepository floorsheetRepository, IRedisRepository redisRepository)
        {
            _floorsheetRepository = floorsheetRepository;
            _mapper = mapper;
            _redisRepository = redisRepository;
        }
        public async Task<PaginatedResult<SellFloorsheet>> Handle(GetSellFloorsheetListQuery request, CancellationToken cancellationToken)
        {

            var (totalCount, pageSize, totalPages, currentPage, floorsheets) = await _floorsheetRepository.GetSellFloorsheets(request);
            return new PaginatedResult<SellFloorsheet>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = floorsheets,
            };

        }
    }
}