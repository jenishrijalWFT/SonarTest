using Boss.Gateway.Domain.Entities;
using MediatR;

namespace Boss.Gateway.Application.Features.JournalVouchers
{
    public class GetJournalVoucherByIdQuery : IRequest<JournalVoucher>
    {
        public Guid JournalVoucherId;
    }
}


