

using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.VoucherTypes;

public class GetvoucherTypeListQueryHandler : IRequestHandler<GetVoucherTypeListQuery, List<VoucherType>>
{

    private readonly IVoucherTypeRepository _voucherRepository;
    private readonly IMapper _mapper;

    public GetvoucherTypeListQueryHandler(IVoucherTypeRepository voucherRepository, IMapper mapper)
    {
        _voucherRepository = voucherRepository;
        _mapper = mapper;
    }

    public Task<List<VoucherType>> Handle(GetVoucherTypeListQuery request, CancellationToken cancellationToken)
    {
        // var result = await _voucherRepository.ListAllAsync();
        // return _mapper.Map<List<VoucherType>>(result);
        throw new NotImplementedException();
    }
}