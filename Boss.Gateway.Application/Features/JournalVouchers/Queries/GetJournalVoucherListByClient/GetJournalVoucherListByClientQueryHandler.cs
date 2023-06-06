using System.Text.Json;
using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.JournalVouchers;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class GetJournalVoucherListByClientQueryHandler : IRequestHandler<GetJournalVoucherListByClientQuery, List<JournalVoucher>>


    {

        private readonly IJournalVoucherRepository _journalvoucherrepository;

        private readonly IRedisRepository _redisRepository;
        private readonly IMapper _mapper;





        public GetJournalVoucherListByClientQueryHandler(IMapper mapper, IJournalVoucherRepository journalVoucherRepository, IRedisRepository redisRepository)
        {
            _journalvoucherrepository = journalVoucherRepository;
            _mapper = mapper;
            _redisRepository = redisRepository;
        }
        public async Task<List<JournalVoucher>> Handle(GetJournalVoucherListByClientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sentryTransaction = SentrySdk.StartTransaction("get-jv-list-by-client", "http-request");

                string RedisKey = $"jv_list:{request.AccountName}";

                var cachedData = await _redisRepository.GetSetMembersAsync(RedisKey);

                if (cachedData.Length <= 0)
                {

                    var data = await _journalvoucherrepository.GetJournalVouchersByClient(request);
                    var dtos = _mapper.Map<List<JournalVoucher>>(data);
                    await _redisRepository.AddToSetAsync(key: RedisKey, value: null, values: dtos.Select(s => JsonSerializer.Serialize(s)).ToArray());
                    sentryTransaction.Finish();

                    return dtos;
                }
                else
                {
                    var decodedList = cachedData.Select(e => JsonSerializer.Deserialize<JournalVoucher>(e)).ToArray();
                    var dtos = _mapper.Map<List<JournalVoucher>>(decodedList).ToList();
                    sentryTransaction.Finish();

                    return dtos;
                }



            }
            catch (Exception ex)
            {
                var customException = new Exception(
                  message: "Get Jv List By Client Query Failed", ex
              );
                customException.AddSentryTag("Floorsheet Command Handler", "Failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}