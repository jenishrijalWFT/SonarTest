using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CMEntries
{
    public class GetCM30EntriesListQueryHandler : IRequestHandler<GetCM30EntriesListQuery, PaginatedResult<CM30Entries>>
    {
        private readonly ICM30Repository _cm30Repository;

        private readonly IMapper _mapper;

        public GetCM30EntriesListQueryHandler(ICM30Repository cM30Repository, IMapper mapper)
        {
            _cm30Repository = cM30Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM30Entries>> Handle(GetCM30EntriesListQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm30Repository.GetCM30Entries(request);
            return new PaginatedResult<CM30Entries>()
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