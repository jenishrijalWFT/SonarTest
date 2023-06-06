using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.JournalVouchers.Queries
{
    public class GetUnbilledTransactionJournalVoucherQuery : IRequest<List<JournalVoucher>>
    {
        public string ClientCode { get; set; } = " ";

    }
}
