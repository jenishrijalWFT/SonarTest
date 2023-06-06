using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface ISectorRepository
{

    Task<int> AddSector(Sector sector);
}