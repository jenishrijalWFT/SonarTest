using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence.CloseOut;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM31EntriesListQueryHandler : IRequestHandler<GetCM31EntriesListQuery, PaginatedResult<CM31Entry>>
    {
        private readonly ICm31Repository _cm31Repository;

        private readonly IMapper _mapper;

        public GetCM31EntriesListQueryHandler(ICm31Repository cM31Repository, IMapper mapper)
        {
            _cm31Repository = cM31Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM31Entry>> Handle(GetCM31EntriesListQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm31Repository.GetCM31Entries(request);
            return new PaginatedResult<CM31Entry>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = data
            };
        }
    }
}