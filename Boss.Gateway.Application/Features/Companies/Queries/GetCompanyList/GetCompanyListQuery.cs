

using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.Companies;

public class GetCompanyListQuery : IRequest<List<Company>> { }