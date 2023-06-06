using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities.CloseOut;
using MediatR;

namespace Boss.Gateway.Application.Features.CM
{
    public class GetCM03ListQueryByIdHandler : IRequestHandler<GetCM03ListQueryById, PaginatedResult<CM03>>
    {
        private readonly ICM03Repository _cM03Repository;
        private readonly IMapper _mapper;

        public GetCM03ListQueryByIdHandler(ICM03Repository cM03Repository, IMapper mapper)
        {
            _cM03Repository = cM03Repository;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CM03>> Handle(GetCM03ListQueryById request, CancellationToken cancellationToken)
        {
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cM03Repository.GetCM03ById(request);
            return new PaginatedResult<CM03>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = data
            }; ;
        }
    }
}