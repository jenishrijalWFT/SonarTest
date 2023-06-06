using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using System.Data;
using Dapper;
using Boss.Gateway.Application.Features.Payments;

namespace Boss.Gateway.Persistence.Repositories
{
    public class AdvancePaymentRepository : IAdvancePayment
    {
        private readonly IDbConnection _dbConnection;

        public AdvancePaymentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<Guid> AddAdvancePayment(AdvancePayment advancePayment)
        {
            string sql = @"INSERT INTO advance_payments (id, date_ad, date_bs, branch, client_name, amount, cheque_no, bank, created_by, remarks) 
                        VALUES (@Id, @DateAd, @DateBs, @Branch, @ClientName, @Amount, @ChequeNo, @Bank, @CreatedBy, @Remarks)";

            Guid advancePaymentId = await _dbConnection.ExecuteScalarAsync<Guid>(sql, advancePayment);
            return advancePaymentId;
        }

        public async Task<List<SellBillTransaction>> GetSellBill(string ClientCode, string DateAd)
        {
            string sql = @"SELECT sb.*, sbt.* FROM sell_bills sb 
               LEFT JOIN sell_bill_transactions sbt ON sb.id = sbt.sell_bill_id  
               WHERE sb.bill_date = @DateAd AND sb.client_code = @ClientCode OR sb.client_name = @ClientCode AND sbt.is_billed = false";

            var sellBillTransactions = new List<SellBillTransaction>();

            await _dbConnection.QueryAsync<SellBill, SellBillTransaction, SellBillTransaction>(
               sql,
               (bill, transaction) =>
               {
                   if (transaction != null)
                   {
                       sellBillTransactions.Add(transaction);
                   }
                   return transaction!;
               },
               new { ClientCode, DateAd },
               splitOn: "id"
            );

            return sellBillTransactions;
        }

        public async Task<List<Guid>> AddAgainstSell(AdvanceAgainstSell advanceOnSell)
        {
            string sql = @"INSERT INTO advance_against_sell (id, transaction_no, advance_amount, advance_payment_id)
                            VALUES (@Id, @TransactionNo, @AdvanceAmount, @AdvancePaymentId)";
            List<Guid> advanceAgainstSell = await _dbConnection.ExecuteScalarAsync<List<Guid>>(sql, advanceOnSell);
            return advanceAgainstSell;
        }

        public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<AdvancePaymentVm> advancePaymentVms)> GetAdvancePayments(GetAdvancePaymentQuery query)
        {
            string countsql = "SELECT COUNT(*) FROM advance_payments";
            int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countsql);
            int skip = (query.page - 1) * query.size;
            string sql = $"SELECT * FROM advance_payments OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY";
            var data = await _dbConnection.QueryAsync<AdvancePaymentVm>(sql);

            int pageSize = query.size;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int currentPage = Math.Min(query.page, totalPages);

            return (totalCount, pageSize, totalPages, currentPage, data.ToList());
        }

        public async Task<AdvancePayment> GetAdvancePaymentDetail(Guid Id)
        {
            string sql = @"
                    SELECT ap.*, ags.*
                    FROM advance_payments ap
                    LEFT JOIN advance_against_sell ags ON ap.id = ags.advance_payment_id
                    WHERE ap.id = @Id";

            var lookUp = new Dictionary<Guid, AdvancePayment>();
            var results = await _dbConnection.QueryAsync<AdvancePayment, AdvanceAgainstSell, AdvancePayment>(
                sql,
                (payment, againstSell) =>
                {
                    if (!lookUp.TryGetValue(payment.Id, out AdvancePayment? advancePayment))
                    {
                        advancePayment = payment;
                        advancePayment.AgainstSell = new List<AdvanceAgainstSell>();
                        lookUp.Add(advancePayment.Id, advancePayment);
                    }

                    if (againstSell != null)
                    {
                        advancePayment.AgainstSell!.Add(againstSell);
                    }

                    return advancePayment;
                },
                new { Id = Id },
                splitOn: "id"
            );

            return lookUp.Values.FirstOrDefault()!;
        }
    }
}