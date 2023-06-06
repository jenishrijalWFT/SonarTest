using Boss.Gateway.Application.Features.JournalVouchers;
using Boss.Gateway.Application.Features.JournalVouchers.Queries;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Contracts.Persistence;

public interface IJournalVoucherRepository
{

    Task<IReadOnlyList<JournalVoucher>> GetJournalVouchersByClient(GetJournalVoucherListByClientQuery query);
    Task<JournalVoucher> GetJournalVoucherById(Guid journalVoucherId);

    Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<JournalVoucher> jvs)>
    GetJournalVouchersList(GetJournalVoucherListQuery query);

    Task<IReadOnlyList<JournalVoucher>> GetUnbilledTransactionJournalVouchers(GetUnbilledTransactionJournalVoucherQuery query);

}