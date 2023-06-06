using System.Text.Json;
using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Sectors;

public class GetSectorListQueryHandler : IRequestHandler<GetSectorListQuery, List<Sector>>
{

    private readonly ISectorRepository _sectorRepository;

    private readonly IRedisRepository _redisRepository;
    private readonly IMapper _mapper;

    private static string RedisKey = "sector_list";

    public GetSectorListQueryHandler(ISectorRepository sectorRepository, IRedisRepository redisRepository, IMapper mapper)
    {
        _sectorRepository = sectorRepository;
        _redisRepository = redisRepository;
        _mapper = mapper;
    }

    public async Task<List<Sector>> Handle(GetSectorListQuery request, CancellationToken cancellationToken)
    {

        var cachedData = await _redisRepository.GetSetMembersAsync(RedisKey);

        if (cachedData.Length <= 0)
        {
            // var sectors = await _sectorRepository.ListAllAsync();
            // TODO
            // var dtos = _mapper.Map<List<Sector>>(sectors);
            // await _redisRepository.AddToSetAsync(key: RedisKey, value: null, values: dtos.Select(s => JsonSerializer.Serialize(s)).ToArray());
            // return dtos;
            throw new NotImplementedException();

        }
        else
        {
            var sentryTransaction = SentrySdk.StartTransaction("get-sector-list", "http-request");
            var decodedList = cachedData.Select(e => JsonSerializer.Deserialize<Sector>(e)).ToArray();
            var dtos = _mapper.Map<List<Sector>>(decodedList);
            sentryTransaction.Finish();
            return dtos;
        }

    }
}