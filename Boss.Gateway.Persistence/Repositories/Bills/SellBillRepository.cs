using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.SellBills;
using Boss.Gateway.Domain.Entities;
using Dapper;

namespace Boss.Gateway.Persistence.Repositories;

public class SellBillRepository : ISellBillRepository
{
    private readonly IDbConnection _dbConnection;

    public SellBillRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<SellBill> GetSellBillById(Guid sellBillId)
    {
        string sql = @"SELECT sb.*, sbt.* FROM sell_bills sb 
                    LEFT JOIN sell_bill_transactions sbt ON sb.id = sbt.sell_bill_id  
                    WHERE sb.id = @SellBillId";
        var lookUp = new Dictionary<Guid, SellBill>();
        var results = await _dbConnection.QueryAsync<SellBill, SellBillTransaction, SellBill>(
           sql,
           (bill, transactions) =>
           {
               if (!lookUp.TryGetValue(bill.Id, out SellBill? sellBill))
               {
                   sellBill = bill;
                   sellBill.Transactions = new List<SellBillTransaction>();
                   lookUp.Add(sellBill.Id, sellBill);
               }

               if (transactions != null)
               {
                   sellBill.Transactions.Add(transactions);
               }
               return sellBill;
           },
           new { sellBillId },
           splitOn: "id"
       );
        return lookUp.Values.ToList().First();
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellBill> bills)> GetsellBills(GetSellBillListQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM sell_bills";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;

        string sql = $@"SELECT sb.*, sbt.*
               FROM (
                   SELECT *
                   FROM sell_bills
                   ORDER BY (SELECT 0)
                   OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
               ) sb
               LEFT JOIN sell_bill_transactions sbt ON sb.id = sbt.sell_bill_id";
        var lookUp = new Dictionary<Guid, SellBill>();
        var results = await _dbConnection.QueryAsync<SellBill, SellBillTransaction, SellBill>(
           sql,
           (bill, transactions) =>
           {
               if (!lookUp.TryGetValue(bill.Id, out SellBill? sellBill))
               {
                   sellBill = bill;
                   sellBill.Transactions = new List<SellBillTransaction>();
                   lookUp.Add(sellBill.Id, sellBill);
               }

               if (transactions != null)
               {
                   sellBill.Transactions.Add(transactions);
               }
               return sellBill;
           },
          new { IsBilled = query.isBilled },
           splitOn: "id"
       );
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, lookUp.Values.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellBill> bills)> GetSellBillsByFloorsheetId(GetSellBillsByFloorsheetIdQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM sell_bills WHERE floorsheet_id = @FloorsheetId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { FloorsheetId = query.FloorsheetId });
        int skip = (query.page - 1) * query.size;

        string sql = $@"SELECT sb.*, sbt.*
               FROM (
                   SELECT *
                   FROM sell_bills where floorsheet_id = @FloorsheetId
                   ORDER BY (SELECT 0)
                   OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY
               ) sb
               LEFT JOIN sell_bill_transactions sbt ON sb.id = sbt.sell_bill_id";
        var lookUp = new Dictionary<Guid, SellBill>();
        var results = await _dbConnection.QueryAsync<SellBill, SellBillTransaction, SellBill>(
           sql,
           (bill, transactions) =>
           {
               if (!lookUp.TryGetValue(bill.Id, out SellBill? sellBill))
               {
                   sellBill = bill;
                   sellBill.Transactions = new List<SellBillTransaction>();
                   lookUp.Add(sellBill.Id, sellBill);
               }

               if (transactions != null)
               {
                   sellBill.Transactions.Add(transactions);
               }
               return sellBill;
           },
          new { IsBilled = query.isBilled, FloorsheetId = query.FloorsheetId },

           splitOn: "id"
       );
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, lookUp.Values.ToList());
    }
}