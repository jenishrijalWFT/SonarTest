using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.CM;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Npgsql;

namespace Boss.Gateway.Persistence.Repositories;

public class CM05Repository : ICM05Repository
{
    private readonly IDbConnection _dbConnection;

    public CM05Repository(IDbConnection dbConnection)
    {

        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }


    private void JvWriter(List<JournalVoucher> journalVouchers)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY journal_vouchers  (id, client_name, client_code,  voucher_date_ad, voucher_date_bs, voucher_no, reference_no, amount, approved_status, created_at, type_id) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in journalVouchers)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.VoucherDateAD, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.VoucherDateBS, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.VoucherNo, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ReferenceNo, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Amount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.ApprovedStatus, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(item.TypeId, NpgsqlTypes.NpgsqlDbType.Uuid);
            }
            writer.Complete();

        }
    }

    private void JvTransactionsWriter(List<JVTransaction> jvTransactions)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY jv_transactions  (id,  description, debit, credit, created_at, updated_at, journal_voucher_id, account_head_id, branch_id) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in jvTransactions)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.Description, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Debit, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.Credit, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(item.UpdatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(item.JournalVoucherId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.AccountHeadId, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BranchId, NpgsqlTypes.NpgsqlDbType.Uuid);
            }
            writer.Complete();
        }
    }

    public async Task CM05Upload(CM05Entries cm05Entries, List<CM05> cm05, List<Company> companies)
    {
        IDbTransaction _transaction = _dbConnection.BeginTransaction();
        try
        {

            var sql = "INSERT INTO cm_05_entries (id, settlement_id, settlement_date_ad, settlement_date_bs, import_date_ad, import_date_bs, created_at) VALUES (@Id, @SettlementID, @SettlementDateAd, @SettlementDateBS, @ImportDateAD, @ImportDateBS, @CreatedAt );SELECT @Id AS Id;";

            await _dbConnection.ExecuteScalarAsync<Guid>(sql, cm05Entries, _transaction);
            using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY cm_05 (id, transaction_number, date_bs, buyer_id, client_name, client_code, stock, quantity, rate, amount, nepse_commission, sebon_commission, tds, adjusted_sell_price, adjusted_purchase_price, cgt, closeout_amount, amount_receivable, created_at, cm05_entry_id) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var item in cm05)
                {
                    writer.StartRow();
                    writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                    writer.Write(item.TransactionNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                    writer.Write(item.DateBS, NpgsqlTypes.NpgsqlDbType.Varchar);
                    writer.Write(item.BuyerId, NpgsqlTypes.NpgsqlDbType.Integer);
                    writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                    writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                    writer.Write(item.Stock, NpgsqlTypes.NpgsqlDbType.Varchar);
                    writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                    writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.Amount, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.NepseCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.SebonCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.TDS, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.AdjustedSellPrice, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.AdjustedPurchasePrice, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.CGT, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.CloseoutAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.AmountReceivable, NpgsqlTypes.NpgsqlDbType.Numeric);
                    writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                    writer.Write(item.CM05EntryId, NpgsqlTypes.NpgsqlDbType.Uuid);
                }
                writer.Complete();
            }


            var UpdateSellBillTransactionsQuery = @"
                UPDATE sell_bill_transactions 
                    SET quantity = cm_05.quantity,
                        rate = cm_05.rate,
                        cgt = cm_05.cgt,
                        wacc_purchase_price= cm_05.adjusted_purchase_price,
                        is_billed = true
                    FROM cm_05
                    WHERE sell_bill_transactions.transaction_number = cm_05.transaction_number;
            ";
            await _dbConnection.ExecuteAsync(UpdateSellBillTransactionsQuery, _transaction);

            var transactionNumbers = cm05.Select(e => e.TransactionNumber).ToList();

            var getTransactionsQuery = @"SELECT * FROM sell_bill_transactions WHERE transaction_number = Any(@TransactionNumbers)";

            var tdata = await _dbConnection.QueryAsync<SellBillTransaction>(getTransactionsQuery, new { TransactionNumbers = transactionNumbers });

            var sellBillIds = tdata.Select(e => e.SellBillId).ToList();


            var getSellBillsQuery = @"
            SELECT sb.*, sbt.*
            FROM sell_bills sb
            LEFT JOIN sell_bill_transactions sbt ON sbt.sell_bill_id = sb.id
            WHERE sb.id = ANY(@SellBillIds)";

            var sellBillsDictionary = new Dictionary<Guid, SellBill>();

            await _dbConnection.QueryAsync<SellBill, SellBillTransaction, SellBill>(
                getSellBillsQuery,
                (sb, sbt) =>
                {
                    if (!sellBillsDictionary.TryGetValue(sb.Id, out var sellBill))
                    {
                        sellBill = sb;
                        sellBill.Transactions = new List<SellBillTransaction>();
                        sellBillsDictionary.Add(sb.Id, sellBill);
                    }

                    sellBill.Transactions.Add(sbt);
                    return sb;
                },
                new { SellBillIds = sellBillIds },
                splitOn: "Id");

            var sellBills = sellBillsDictionary.Values.ToList();

            var journalVouchers = new List<JournalVoucher>();
            var jvTransactions = new List<JVTransaction>();


            var sellBillList = new List<SellBill>();

            var clientCodeList = sellBillList.Select(e => e.ClientCode).Distinct().ToArray();
            var sellBillTransaction = new List<SellBillTransaction>();

            // TODO : Fiscal Year

            foreach (var sellbill in sellBills)
            {
                var firstPart = sellbill.BillNumber.Substring(2);
                var jvNumber = firstPart.Substring(0, firstPart.IndexOf("/"));

                var TDS = (sellbill.TotalCommission - sellbill.NepseCommission - sellbill.SeboRegulatoryFee) * 0.15m;
                var nepseReceivalbe = (sellbill.NetPayableAmount + sellbill.TotalCommission + sellbill.DpAmount + sellbill.SeboRegulatoryFee) - (TDS + sellbill.NepseCommission + sellbill.SeboRegulatoryFee);


                JournalVoucher journalVoucher = new JournalVoucher()
                {
                    ClientName = sellbill.ClientName,
                    ClientCode = sellbill.ClientCode,
                    ReferenceNo = sellbill.BillNumber,
                    VoucherDateBS = cm05Entries.ImportDateBs!,
                    VoucherDateAD = cm05Entries.ImportDateAd!,
                    TypeId = Guid.Parse("ff126576-4c63-4b0a-a446-9b746f010ce7"),
                    VoucherNo = $"JV/{jvNumber}/S/2079-80",
                    Amount = sellbill.Transactions.Sum(t => t.Amount)
                };
                journalVouchers.Add(journalVoucher);

                string descriptioinData = "";

                List<SellBillTransaction> description = new List<SellBillTransaction>();
                foreach (var transaction in sellbill.Transactions)
                {
                    var symbol = companies.Where(c => c.Name == transaction.CompanyName).Select(c => c.Symbol).FirstOrDefault();

                    bool foundMatch = false;
                    if (transaction.SellBillId == sellbill.Id)
                    {
                        foreach (var item in description)
                        {

                            if (symbol == item.Symbol && transaction.Rate == item.Rate)
                            {
                                item.Symbol = symbol;
                                item.Quantity += transaction.Quantity;
                                item.Rate = transaction.Rate;
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            description.Add(new SellBillTransaction
                            {
                                Symbol = symbol!,
                                Rate = transaction.Rate,
                                Quantity = transaction.Quantity
                            });
                        }
                    }
                }

                if (journalVoucher.ReferenceNo == sellbill.BillNumber)
                {
                    foreach (var item in description)
                    {
                        descriptioinData += $"{item.Symbol} {item.Quantity} kitta @ {item.Rate}, ";
                    }
                }


                JVTransaction NepseReceivableJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Nepse receivable for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Credit = nepseReceivalbe,
                    AccountHeadId = "6"
                };
                jvTransactions.Add(NepseReceivableJvTransaction);

                JVTransaction StockCommissionJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Stock commission for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Debit = sellbill.NepseCommission,
                    AccountHeadId = "5"
                };
                jvTransactions.Add(StockCommissionJvTransaction);

                JVTransaction TdsJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"TDS for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Debit = TDS,
                    AccountHeadId = "4"
                };
                jvTransactions.Add(TdsJvTransaction);

                JVTransaction BrokerSebonJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Sebon regulatory fee for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Debit = sellbill.SeboRegulatoryFee,
                    AccountHeadId = "5a"
                };
                jvTransactions.Add(BrokerSebonJvTransaction);

                JVTransaction CommissionIncomeJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Commission income for purchase bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Credit = sellbill.TotalCommission,
                    AccountHeadId = "3"
                };
                jvTransactions.Add(CommissionIncomeJvTransaction);

                JVTransaction ClientJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Payable to client for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Debit = sellbill.NetPayableAmount,
                    AccountHeadId = sellbill.ClientCode
                };
                jvTransactions.Add(ClientJvTransaction);

                JVTransaction DpJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"DP fee for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Credit = sellbill.DpAmount,
                    AccountHeadId = "8"
                };
                jvTransactions.Add(DpJvTransaction);

                JVTransaction SebonPayableJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Sebon regulatory fee payable for sell bill no. {sellbill.BillNumber} ({descriptioinData})",
                    Credit = sellbill.SeboRegulatoryFee,
                    AccountHeadId = "5b"
                };
                jvTransactions.Add(SebonPayableJvTransaction);
            }

            JvWriter(journalVouchers);
            JvTransactionsWriter(jvTransactions);


            _transaction.Commit();
        }
        catch (Exception e)
        {
            _transaction.Rollback();

            throw new Exception(e.StackTrace);
        }



    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM05> data)> GetCM05ById(GetCM05ListQueryById query)
    {
        string countSql = @"SELECT COUNT(*) FROM cm_05 WHERE cm05_entry_id = @cM05EntriesId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { cM05EntriesId = query.CM05EntriesId });
        int skip = (query.page - 1) * query.size;
        string sql = @"SELECT * FROM cm_05 WHERE cm05_entry_id = @cM05EntriesId" + $" ORDER BY(SELECT 0) OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
        var data = await _dbConnection.QueryAsync<CM05>(sql, new { cM05EntriesId = query.CM05EntriesId });
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, data.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<CM05Entries> data)> GetCM05Entries(GetCM05EntriesListQuery query)
    {
        string countSql = @"SELECT COUNT(*) FROM cm_05_entries";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;
        string sql = "SELECT * FROM cm_05_entries" + $" ORDER BY(SELECT 0) OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
        var data = await _dbConnection.QueryAsync<CM05Entries>(sql);
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, data.ToList());
    }
}
