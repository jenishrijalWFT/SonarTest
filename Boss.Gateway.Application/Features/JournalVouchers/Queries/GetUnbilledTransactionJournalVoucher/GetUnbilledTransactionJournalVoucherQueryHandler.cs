using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.JournalVouchers.Queries
{
    public class GetUnbilledTransactionJournalVoucherQueryHandler : IRequestHandler<GetUnbilledTransactionJournalVoucherQuery, List<JournalVoucher>>
    {
        private readonly IJournalVoucherRepository _journalVoucherRepository;

        public GetUnbilledTransactionJournalVoucherQueryHandler(IJournalVoucherRepository journalVoucherRepository)
        {
            _journalVoucherRepository = journalVoucherRepository;

        }
        public async Task<List<JournalVoucher>> Handle(GetUnbilledTransactionJournalVoucherQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-unbilled-transaction", "http-request");
                var data = await _journalVoucherRepository.GetUnbilledTransactionJournalVouchers(request);
                var dtos = data.ToList();
                sentryTransaction.Finish();

                return dtos;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                 message: "Get Unbilled Transactiom Query Failed", ex
             );
                customException.AddSentryTag("Get Unbilled Transaction Query Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }

    }
}

