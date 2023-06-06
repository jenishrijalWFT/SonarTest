using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence.CloseOut;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM31ListQueryByIdHandler : IRequestHandler<GetCM31ListQueryById, PaginatedResult<CM31>>
    {
        private readonly ICm31Repository _cM31Repository;
        private readonly IMapper _mapper;

        public GetCM31ListQueryByIdHandler(ICm31Repository cM31Repository, IMapper mapper)
        {
            _cM31Repository = cM31Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM31>> Handle(GetCM31ListQueryById request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cM31Repository.GetCM31ById(request);
            return new PaginatedResult<CM31>()
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