using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM05ListQueryByIdHandler : IRequestHandler<GetCM05ListQueryById, PaginatedResult<CM05>>
    {
        private readonly ICM05Repository _cm05Repository;
        private readonly IMapper _mapper;

        public GetCM05ListQueryByIdHandler(ICM05Repository cm05Repository, IMapper mapper)
        {
            _cm05Repository = cm05Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM05>> Handle(GetCM05ListQueryById request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm05Repository.GetCM05ById(request);
            return new PaginatedResult<CM05>()
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