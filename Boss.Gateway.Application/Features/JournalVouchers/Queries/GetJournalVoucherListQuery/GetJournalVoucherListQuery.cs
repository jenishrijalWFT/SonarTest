using MediatR;

namespace Boss.Gateway.Application.Features.JournalVouchers;

public class GetJournalVoucherListQuery : IRequest<PaginatedResult<JournalVoucherListVm>>
{
    public int page { get; set; } = 1;
    public int size { get; set; } = 40;

}
