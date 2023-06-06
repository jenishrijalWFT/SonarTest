using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.PurchaseBills;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Sentry;

namespace Boss.Gateway.Persistence.Repositories;


public class PurchaseBillRepository : IPurchaseBillRepository
{

    private readonly IDbConnection _dbConnection;

    public PurchaseBillRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<PurchaseBill> GetPurchaseBillById(Guid PurchaseBillId)
    {

        var sentryTransaction = SentrySdk.StartTransaction("get-purchase-bill-list-by-id", "db");
        string sql = @"SELECT pb.*, pbt.*
                   FROM purchase_bills pb
                   LEFT JOIN purchase_bill_transactions pbt ON pb.id = pbt.purchase_bill_id
                   WHERE pb.id = @PurchaseBillId;";

        var lookup = new Dictionary<Guid, PurchaseBill>();
        var results = await _dbConnection.QueryAsync<PurchaseBill, PurchaseBillTransaction, PurchaseBill>(
            sql,
            (bill, transactions) =>
            {
                if (!lookup.TryGetValue(bill.Id, out PurchaseBill? purchaseBill))
                {
                    purchaseBill = bill;
                    purchaseBill.Transactions = new List<PurchaseBillTransaction>();
                    lookup.Add(purchaseBill.Id, purchaseBill);
                }

                if (transactions != null)
                {
                    purchaseBill.Transactions.Add(transactions);
                }

                return purchaseBill;
            },
            new { PurchaseBillId = PurchaseBillId },
            splitOn: "id"
        );

        return lookup.Values.ToList().First();



    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<PurchaseBill> bills)> GetPurchaseBills(GetPurchaseBillListQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM purchase_bills";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;
        string sql = $@"SELECT pb.*, pbt.*
               FROM (
                   SELECT *
                   FROM purchase_bills
                   ORDER BY (SELECT 0)
                   OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
               ) pb
               LEFT JOIN purchase_bill_transactions pbt ON pb.id = pbt.purchase_bill_id";
        var lookup = new Dictionary<Guid, PurchaseBill>();
        var results = await _dbConnection.QueryAsync<PurchaseBill, PurchaseBillTransaction, PurchaseBill>(
            sql,
            (bill, transactions) =>
            {
                if (!lookup.TryGetValue(bill.Id, out PurchaseBill? purchaseBill))
                {
                    purchaseBill = bill;
                    purchaseBill.Transactions = new List<PurchaseBillTransaction>();
                    lookup.Add(purchaseBill.Id, purchaseBill);
                }

                if (transactions != null)
                {
                    purchaseBill.Transactions.Add(transactions);
                }

                return purchaseBill;
            },

            splitOn: "id"
        );


        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);

        return (totalCount, pageSize, totalPages, currentPage, lookup.Values.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<PurchaseBill> bills)> GetPurchaseBillsByFloorsheetId(GetPurchaseBillsByFloorsheetIdQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM purchase_bills WHERE floorsheet_id = @FloorsheetId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { FloorsheetId = query.FloorsheetId });
        int skip = (query.page - 1) * query.size;
        string sql = $@"SELECT pb.*, pbt.*
               FROM (
                   SELECT *
                   FROM purchase_bills WHERE floorsheet_id = @FloorsheetId
                   ORDER BY (SELECT 0)
                   OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
               ) pb
               LEFT JOIN purchase_bill_transactions pbt ON pb.id = pbt.purchase_bill_id";
        var lookup = new Dictionary<Guid, PurchaseBill>();
        var results = await _dbConnection.QueryAsync<PurchaseBill, PurchaseBillTransaction, PurchaseBill>(
            sql,
            (bill, transactions) =>
            {
                if (!lookup.TryGetValue(bill.Id, out PurchaseBill? purchaseBill))
                {
                    purchaseBill = bill;
                    purchaseBill.Transactions = new List<PurchaseBillTransaction>();
                    lookup.Add(purchaseBill.Id, purchaseBill);
                }

                if (transactions != null)
                {
                    purchaseBill.Transactions.Add(transactions);
                }

                return purchaseBill;
            },
            new { FloorsheetId = query.FloorsheetId },
            splitOn: "id"
        );


        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);

        return (totalCount, pageSize, totalPages, currentPage, lookup.Values.ToList());
    }
}