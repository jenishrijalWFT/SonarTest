using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;



public interface IVoucherTypeRepository
{
    Task AddVoucherTypeName(VoucherType voucherType);
}