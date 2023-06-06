using System.Text.Json;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Sectors;


public class CreateSectorCommandHandler : IRequestHandler<CreateSectorCommand, CreateSectorCommandResponse>
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IRedisRepository _redisRepository;
    private static string RedisKey = "sector_list";
    public CreateSectorCommandHandler(IRedisRepository redisRepository, ISectorRepository sectorRepository)
    {
        _sectorRepository = sectorRepository;
        _redisRepository = redisRepository;
    }

    public async Task<CreateSectorCommandResponse> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("create-sector", "http-request");
            var createSectorCommandResponse = new CreateSectorCommandResponse();
            var validator = new CreateSectorCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                createSectorCommandResponse.Success = false;
                createSectorCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createSectorCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createSectorCommandResponse.Success)
            {
                var sector = new Sector() { Name = request.Name, Code = request.Code };
                await _sectorRepository.AddSector(sector);
                await _redisRepository.AddToSetAsync(key: RedisKey, value: JsonSerializer.Serialize(sector), values: null);
                createSectorCommandResponse.Message = "New sector added successfully";
            }
            sentryTransaction.Finish();

            return createSectorCommandResponse;
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                 message: " New Sector Creation Command Failed", ex
             );
            customException.AddSentryTag("Create Sector Command Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }
    }
}