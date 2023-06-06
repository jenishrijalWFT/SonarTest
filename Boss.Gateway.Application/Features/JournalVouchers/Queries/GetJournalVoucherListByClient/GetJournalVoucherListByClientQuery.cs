using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.JournalVouchers;

public class GetJournalVoucherListByClientQuery : IRequest<List<JournalVoucher>>
{
    public string AccountName { get; set; } = "";
}