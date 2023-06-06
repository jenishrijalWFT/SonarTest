using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.CM;

public class GetCM05EntriesListQueryHandler : IRequestHandler<GetCM05EntriesListQuery, PaginatedResult<CM05Entries>>
{

    private readonly ICM05Repository _cm05Repository;
    private readonly IMapper _mapper;

    public GetCM05EntriesListQueryHandler(ICM05Repository cm05Repository, IMapper mapper)
    {
        _cm05Repository = cm05Repository;
        _mapper = mapper;
    }


    public async Task<PaginatedResult<CM05Entries>> Handle(GetCM05EntriesListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("get-cm-05-list", "http-request");
            var (totalCount, pageSize, totalPages, currentPage, data) = await _cm05Repository.GetCM05Entries(request);
            return new PaginatedResult<CM05Entries>()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = currentPage,
                Data = data
            };
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                  message: "Get CM05 Query Failed", ex
              );
            customException.AddSentryTag("CM05 Query Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }
    }
}