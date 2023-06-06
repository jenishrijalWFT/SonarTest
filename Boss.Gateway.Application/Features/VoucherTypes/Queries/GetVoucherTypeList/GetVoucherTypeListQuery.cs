

using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.VoucherTypes;


public class GetVoucherTypeListQuery : IRequest<List<VoucherType>> {}