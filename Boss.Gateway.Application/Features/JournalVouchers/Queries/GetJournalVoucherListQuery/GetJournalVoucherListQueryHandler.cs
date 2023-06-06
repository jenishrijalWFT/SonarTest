

using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using MediatR;

namespace Boss.Gateway.Application.Features.JournalVouchers;

public class GetJournalVoucherListQueryHandler : IRequestHandler<GetJournalVoucherListQuery, PaginatedResult<JournalVoucherListVm>>
{

    private readonly IJournalVoucherRepository _JournalVoucherRepository;
    private readonly IMapper _mapper;

    public GetJournalVoucherListQueryHandler(IJournalVoucherRepository JournalVoucherRepository, IMapper mapper)
    {
        _JournalVoucherRepository = JournalVoucherRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<JournalVoucherListVm>> Handle(GetJournalVoucherListQuery request, CancellationToken cancellationToken)
    {
        var (totalCount, pageSize, totalPages, currentPage, jvs) = await _JournalVoucherRepository.GetJournalVouchersList(request);

        var jvVms = new List<JournalVoucherListVm>();


        foreach (var jv in jvs)
        {
            jvVms.Add(new JournalVoucherListVm()
            {
                Id = jv.Id,
                ClientCode = jv.ClientCode,
                ClientName = jv.ClientName,
                VoucherDateAD = jv.VoucherDateAD,
                VoucherDateBS = jv.VoucherDateBS,
                VoucherNo = jv.VoucherNo,
                ReferenceNo = jv.ReferenceNo,
                Amount = jv.Amount,
                ApprovedStatus = jv.ApprovedStatus,
                VoucherType = jv.VoucherType!.Type!,
                BalanceAmount = jv.BalanceAmount.ToString(),
                CreatedAt = jv.CreatedAt,

            });
        }
        return new PaginatedResult<JournalVoucherListVm>()
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            PageSize = pageSize,
            CurrentPage = currentPage,
            Data = jvVms,
        };
    }
}
