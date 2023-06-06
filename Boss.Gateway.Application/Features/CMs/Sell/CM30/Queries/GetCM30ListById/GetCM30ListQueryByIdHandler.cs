using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM30ListByIdQueryHandler : IRequestHandler<GetCM30ListByIdQuery, PaginatedResult<CM30>>
    {
        private readonly ICM30Repository _cm30Repository;

        private readonly IMapper _mapper;

        public GetCM30ListByIdQueryHandler(ICM30Repository cM30Repository, IMapper mapper)
        {
            _cm30Repository = cM30Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM30>> Handle(GetCM30ListByIdQuery request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm30Repository.GetCM30ById(request);
            return new PaginatedResult<CM30>()
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