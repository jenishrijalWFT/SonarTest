using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.Companies;

public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, List<Company>>
{
    private readonly ICompanyRepository _CompanyRepository;

    public GetCompanyListQueryHandler(ICompanyRepository CompanyRepository)
    {
        _CompanyRepository = CompanyRepository;

    }

    public async Task<List<Company>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
    {

        var result = await _CompanyRepository.GetCompanyList();
        return result.ToList();
    }
}