using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.JournalVouchers
{
    public class GetJournalVoucherByIdQueryHandler : IRequestHandler<GetJournalVoucherByIdQuery, JournalVoucher>
    {
        private readonly IJournalVoucherRepository _journalVoucherRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IMapper _mapper;

        public GetJournalVoucherByIdQueryHandler(IJournalVoucherRepository journalVoucherRepository, IRedisRepository redisRepository, IMapper mapper)
        {
            _journalVoucherRepository = journalVoucherRepository;
            _redisRepository = redisRepository;
            _mapper = mapper;
        }

        public async Task<JournalVoucher> Handle(GetJournalVoucherByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-jv-by-id", "http-request");
                var data = await _journalVoucherRepository.GetJournalVoucherById(request.JournalVoucherId);
                data.JVTransactions = data.JVTransactions.Select(e =>
                {
                    e.Debit = Math.Round(e.Debit, 2);
                    e.Credit = Math.Round(e.Credit, 2);
                    return e;
                }).ToList();
                sentryTransaction.Finish();
                return data;
            }
            catch (Exception ex)
            {
                var customException = new Exception(
                 message: "Get Jv By Id Query Failed", ex
             );
                customException.AddSentryTag("Get JV By ID Query Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }


    }
}
