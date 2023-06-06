using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM03EntriesListQueryHandler : IRequestHandler<GetCM03EntriesListQuery, PaginatedResult<CM03Entry>>
    {
        private readonly ICM03Repository _cm03Repository;

        private readonly IMapper _mapper;

        public GetCM03EntriesListQueryHandler(ICM03Repository cM03Repository, IMapper mapper)
        {
            _cm03Repository = cM03Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM03Entry>> Handle(GetCM03EntriesListQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm03Repository.GetCM03Entries(request);
            return new PaginatedResult<CM03Entry>()
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