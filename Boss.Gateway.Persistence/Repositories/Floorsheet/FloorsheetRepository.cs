
using System.Data;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.FloorSheets;
using Boss.Gateway.Domain.Entities;
using Dapper;
using Npgsql;

namespace Boss.Gateway.Persistence.Repositories;
public class FloorsheetRepository : IFloorsheetRepository
{
    private readonly IDbConnection _dbConnection;

    public FloorsheetRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    List<BrokerageCommission> brokerCommissions = new List<BrokerageCommission>() {
        new BrokerageCommission(){
            InstrumentType = "Non-Convertible Debentures",
            MinRange = 5000001,
            MaxRange = 1000000000,
            BrokeragePercent = (decimal)0.02,
        },
        new BrokerageCommission(){
            InstrumentType = "Non-Convertible Debentures",
            MinRange = 500001,
            MaxRange = 5000000,
            BrokeragePercent = (decimal)0.05,
        },
        new BrokerageCommission(){
            InstrumentType = "Non-Convertible Debentures",
            MinRange = 1,
            MaxRange = 500000,
            BrokeragePercent = (decimal)0.1,
        },

         new BrokerageCommission(){
            InstrumentType = "Preference Shares",
            MinRange = 10000001,
            MaxRange = 1000000000,
            BrokeragePercent = (decimal)0.27,
        },
          new BrokerageCommission(){
            InstrumentType = "Preference Shares",
            MinRange = 2000001,
            MaxRange = 10000000,
            BrokeragePercent = (decimal)0.3,
        },
          new BrokerageCommission(){
            InstrumentType = "Preference Shares",
            MinRange = 500001,
            MaxRange = 2000000,
            BrokeragePercent = (decimal)0.34,
        },
          new BrokerageCommission(){
            InstrumentType = "Preference Shares",
            MinRange = 50001,
            MaxRange = 500000,
            BrokeragePercent = (decimal)0.37,
        },
          new BrokerageCommission(){
            InstrumentType = "Preference Shares",
            MinRange = 1,
            MaxRange = 50000,
            BrokeragePercent = (decimal)0.4,
        },

         new BrokerageCommission(){
            InstrumentType = "Mutual Funds",
            MinRange = 5000001,
            MaxRange = 1000000000,
            BrokeragePercent = (decimal)0.1,
        },
         new BrokerageCommission(){
            InstrumentType = "Mutual Funds",
            MinRange = 500001,
            MaxRange = 5000000,
            BrokeragePercent = (decimal)0.12,
        },
         new BrokerageCommission(){
            InstrumentType = "Mutual Funds",
            MinRange = 1,
            MaxRange = 500000,
            BrokeragePercent = (decimal)0.15,
        },

        new BrokerageCommission(){
            InstrumentType = "Equity",
            MinRange = 10000001,
            MaxRange = 1000000000,
            BrokeragePercent = (decimal)0.27,
        },
         new BrokerageCommission(){
            InstrumentType = "Equity",
            MinRange = 2000001,
            MaxRange = 10000000,
            BrokeragePercent = (decimal)0.3,
        },
         new BrokerageCommission(){
            InstrumentType = "Equity",
            MinRange = 500001,
            MaxRange = 2000000,
            BrokeragePercent = (decimal)0.34,
        },

         new BrokerageCommission(){
            InstrumentType = "Equity",
            MinRange = 50001,
            MaxRange = 500000,
            BrokeragePercent = (decimal)0.37,
        },

         new BrokerageCommission(){
            InstrumentType = "Equity",
            MinRange = 1,
            MaxRange = 50000,
            BrokeragePercent = (decimal)0.4,
        },

    };

    private decimal GetCommissionRate(string instrumentType, decimal amount)
    {

        var commission = brokerCommissions
        .Where(c => c.InstrumentType == instrumentType && c.MinRange <= amount && c.MaxRange >= amount)
        .FirstOrDefault();
        return commission == null ? 0 : commission.BrokeragePercent;
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





    private void PurchaseBillTransactionsWriter(List<PurchaseBillTransaction> purchaseBillTransactions)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY purchase_bill_transactions  (id, transaction_number, company_name, quantity, rate, commission_rate, sebo_commision, eff_rate, co_quantity, co_amount, purchase_bill_id, created_at ) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in purchaseBillTransactions)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.TransactionNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.CompanyName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CommissionRate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboCommision, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.EffRate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CoQuantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.CoAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.PurchaseBillId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);

            }
            writer.Complete();
        }
    }


    private void PurchaseBillWriter(List<PurchaseBill> purchaseBills)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY purchase_bills (id, client_code, client_name, bill_number, bill_date, broker_commission, nepse_commission, sebo_commission, sebo_regulatory_fee, clearance_date, dp_amount, floorsheet_id, created_at)  FROM STDIN (FORMAT BINARY) "))
        {
            foreach (var item in purchaseBills)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BillNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BillDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BrokerCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.NepseCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboRegulatoryFee, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.ClearanceDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.DpAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.FloorsheetId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
            }
            writer.Complete();
        }
    }
    private void SellFloorsheetWriter(List<SellFloorsheet> sellFloorsheets, Guid floorsheetId)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY sell_floorsheets (id, contract_no, symbol, buyer, seller, client_name, client_code, quantity, rate, stock_commission, floorsheet_id, created_at) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in sellFloorsheets)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.ContractNo, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Symbol, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Buyer, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Seller, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.StockCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(floorsheetId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
            }
            writer.Complete();
        }
    }

    private void BuyFloorsheetWriter(List<BuyFloorsheet> buyFloorsheets, Guid floorsheetId)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY buy_floorsheets (id, contract_no, symbol, buyer, seller, client_name, client_code, quantity, rate, stock_commission, bank_deposit, floorsheet_id, created_at) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in buyFloorsheets)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.ContractNo, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Symbol, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Buyer, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Seller, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.StockCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.BankDeposit, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(floorsheetId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
            }
            writer.Complete();
        }

    }

    private void SellBillTransactionsWriter(List<SellBillTransaction> sellBillTransactions)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport("COPY sell_bill_transactions (id, transaction_number, company_name, quantity, rate, commission_rate, cgt, sebo_commission, wacc_purchase_price, effective_rate, co_quantity, co_amount, created_at, sell_bill_id, is_billed) FROM STDIN (FORMAT BINARY)"))
        {
            foreach (var item in sellBillTransactions)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.TransactionNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.CompanyName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.Quantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.Rate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CommissionRate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CGT, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.WaccPurchasePrice, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.EffectiveRate, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CoQuantity, NpgsqlTypes.NpgsqlDbType.Integer);
                writer.Write(item.CoAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
                writer.Write(item.SellBillId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.IsBilled, NpgsqlTypes.NpgsqlDbType.Boolean);
            }
            writer.Complete();
        }
    }

    private void SellBillWriter(List<SellBill> sellBills)
    {
        using (var writer = ((NpgsqlConnection)_dbConnection).BeginBinaryImport(@"COPY sell_bills (id, 
        client_code ,client_name, 
        bill_number, bill_date, broker_commission, nepse_commission, sebo_commission, sebo_regulatory_fee, clearance_date, dp_amount, floorsheet_id, created_at) FROM STDIN(FORMAT BINARY)"))
        {
            foreach (var item in sellBills)
            {
                writer.StartRow();
                writer.Write(item.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.ClientCode, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.ClientName, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BillNumber, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BillDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.BrokerCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.NepseCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboCommission, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.SeboRegulatoryFee, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.ClearanceDate, NpgsqlTypes.NpgsqlDbType.Varchar);
                writer.Write(item.DpAmount, NpgsqlTypes.NpgsqlDbType.Numeric);
                writer.Write(item.FloorsheetId, NpgsqlTypes.NpgsqlDbType.Uuid);
                writer.Write(item.CreatedAt, NpgsqlTypes.NpgsqlDbType.Timestamp);
            }
            writer.Complete();
        }
    }




    public async Task FloorsheetEntry(Floorsheet floorsheet, List<BuyFloorsheet> buyFloorsheets, List<SellFloorsheet> sellFloorsheets, List<Company> companies)
    {
        // var trasnsaction = SentrySdk.StartTransaction("add-floorsheet", "db");
        try
        {
            var _transaction = (NpgsqlTransaction)_dbConnection.BeginTransaction();
            var sql = "INSERT INTO floorsheets (id, fiscal_year, floorsheet_date_ad, floorsheet_date_bs, import_date_ad, import_date_bs, amount, stock_commission, bank_deposit, created_at) VALUES (@Id, @FiscalYear, @FloorsheetDateAd, @FloorsheetDateBs, @ImportDateAd, @ImportDateBs, @Amount, @StockCommission, @BankDeposit, @CreatedAt); SELECT @Id AS Id;";
            await _dbConnection.ExecuteScalarAsync<Guid>(sql, floorsheet, _transaction);


            var purchaseBillTransactions = new List<PurchaseBillTransaction>();
            var purchaseBills = new List<PurchaseBill>();

            var sellBillTransactions = new List<SellBillTransaction>();
            var sellBills = new List<SellBill>();

            var journalVouchers = new List<JournalVoucher>();
            var jvTransactions = new List<JVTransaction>();

            var clientCodeList = buyFloorsheets.Select(e => e.ClientCode).Distinct().ToArray();
            var sellClientCodeList = sellFloorsheets.Select(e => e.ClientCode).Distinct().ToArray();


            var sellBillSql = "SELECT bill_number FROM sell_bills ORDER BY created_at DESC LIMIT 1";
            var lastSellBill = await _dbConnection.QueryFirstOrDefaultAsync<string>(sellBillSql);

            var billNumberSql = "SELECT bill_number FROM purchase_bills ORDER BY created_at DESC LIMIT 1";
            var lastBillNumber = await _dbConnection.QueryFirstOrDefaultAsync<string?>(billNumberSql);

            int billStartCount;
            int sellStartCount;

            if (lastBillNumber is null)
            {
                billStartCount = 1;
            }
            else
            {
                var firstPart = lastBillNumber!.Substring(2);
                var secondPart = firstPart.Substring(0, firstPart.IndexOf("/"));
                billStartCount = int.Parse(secondPart) + 1;
            }

            //sell bill number
            if (lastSellBill is null)
            {
                sellStartCount = 1;
            }
            else
            {
                var firstPart = lastSellBill!.Substring(2);
                var secondPart = firstPart.Substring(0, firstPart.IndexOf("/"));
                sellStartCount = int.Parse(secondPart) + 1;
            }

            foreach (var clientCode in clientCodeList)
            {
                var clientBuys = new List<BuyFloorsheet>();
                var clientPurchases = new List<PurchaseBillTransaction>();
                foreach (var data in buyFloorsheets)
                {
                    if (clientCode == data.ClientCode)
                    {
                        clientBuys.Add(data);
                    }
                }

                PurchaseBill purchaseBill = new PurchaseBill()
                {
                    ClientCode = clientCode,
                    ClientName = clientBuys[0].ClientName,
                    BillNumber = $"B/{billStartCount}/{floorsheet.FiscalYear}",
                    BillDate = floorsheet.FloorsheetDateAd!,
                    ClearanceDate = floorsheet.ImportDateAd!,
                    FloorsheetId = floorsheet.Id,
                };

                foreach (var buy in clientBuys)
                {
                    var InstrumentType = companies.Where(c => c.Symbol == buy.Symbol).Select(c => c.InstrumentType).FirstOrDefault()!;
                    PurchaseBillTransaction purchaseBillTransaction = new PurchaseBillTransaction()
                    {
                        TransactionNumber = buy.ContractNo!,
                        Quantity = (int)buy.Quantity!,
                        Rate = (decimal)buy.Rate!,
                        CompanyName = companies.Where(c => c.Symbol == buy.Symbol).Select(c => c.Name).FirstOrDefault() ?? "",
                        Symbol = buy.Symbol,
                        PurchaseBillId = purchaseBill.Id
                    };
                    purchaseBillTransaction.CommissionRate = GetCommissionRate(InstrumentType, purchaseBillTransaction.Amount);
                    purchaseBillTransaction.SeboCommision = Math.Round(((decimal)0.00015 * purchaseBillTransaction.Amount), 2);
                    purchaseBillTransaction.EffRate = purchaseBillTransaction.Total / purchaseBillTransaction.Quantity;
                    purchaseBillTransactions.Add(purchaseBillTransaction);
                }

                purchaseBill.Transactions = purchaseBillTransactions.Where(t => t.PurchaseBillId == purchaseBill.Id).ToList();
                var CommissionAmount = purchaseBill.Transactions.Sum(s => s.CommissionAmount);
                purchaseBill.NepseCommission = Math.Round((0.2m * CommissionAmount), 2);
                purchaseBill.SeboRegulatoryFee = Math.Round((0.006m * CommissionAmount), 2);
                purchaseBill.BrokerCommission = Math.Round((0.794m * CommissionAmount), 2);
                purchaseBill.DpAmount = purchaseBill.Transactions.Select(e => e.CompanyName).Distinct().ToArray().Count() * 25;
                purchaseBill.SeboCommission = Math.Round((0.00015m * purchaseBill.ShareAmount), 2);

                purchaseBills.Add(purchaseBill);

                var tds = (CommissionAmount - purchaseBill.NepseCommission - purchaseBill.SeboRegulatoryFee) * 0.15m;
                var nepsePayable = (purchaseBill.NetReceivableAmount + tds + purchaseBill.NepseCommission + purchaseBill.SeboRegulatoryFee) - (CommissionAmount + purchaseBill.DpAmount + purchaseBill.SeboRegulatoryFee);

                JournalVoucher journalVoucher = new JournalVoucher()
                {
                    ClientName = purchaseBill.ClientName,
                    ClientCode = purchaseBill.ClientCode,
                    ReferenceNo = purchaseBill.BillNumber,
                    VoucherDateBS = floorsheet.FloorsheetDateBs!,
                    VoucherDateAD = floorsheet.FloorsheetDateBs!,
                    TypeId = Guid.Parse("01755f89-1daa-4a48-9112-f890e682cb83"),
                    VoucherNo = $"JV/{billStartCount}/B/{floorsheet.FiscalYear}",
                    Amount = purchaseBill.Transactions.Sum(t => t.Amount)
                };

                journalVouchers.Add(journalVoucher);

                string descriptioinData = "";

                List<PurchaseBillTransaction> description = new List<PurchaseBillTransaction>();
                foreach (var transaction in purchaseBillTransactions)
                {
                    bool foundMatch = false;
                    if (transaction.PurchaseBillId == purchaseBill.Id)
                    {
                        foreach (var item in description)
                        {
                            if (transaction.Symbol == item.Symbol && transaction.Rate == item.Rate)
                            {
                                item.Symbol = transaction.Symbol;
                                item.Quantity += transaction.Quantity;
                                item.Rate = transaction.Rate;
                                foundMatch = true;
                                break;
                            }
                        }

                        if (!foundMatch)
                        {
                            description.Add(new PurchaseBillTransaction
                            {
                                Symbol = transaction.Symbol,
                                Rate = transaction.Rate,
                                Quantity = transaction.Quantity
                            });
                        }
                    }
                }

                if (journalVoucher.ReferenceNo == purchaseBill.BillNumber)
                {
                    foreach (var item in description)
                    {
                        descriptioinData += $"{item.Symbol} {item.Quantity} kitta @ {item.Rate}, ";
                    }
                }


                JVTransaction ClientJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Receivable from client for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData}) )",
                    Debit = purchaseBill.NetReceivableAmount,
                    AccountHeadId = clientCode
                };

                jvTransactions.Add(ClientJvTransaction);

                JVTransaction StockExchangeJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Stock commission for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData})",
                    Debit = purchaseBill.NepseCommission,
                    AccountHeadId = "5"
                };

                jvTransactions.Add(StockExchangeJvTransaction);

                JVTransaction TdsJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"TDS for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData})",
                    Debit = tds,
                    AccountHeadId = "4"
                };

                jvTransactions.Add(TdsJvTransaction);

                JVTransaction BrokerSeboJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Sebon regulatory fee for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData})",
                    Debit = purchaseBill.SeboRegulatoryFee,
                    AccountHeadId = "5a"
                };

                jvTransactions.Add(BrokerSeboJvTransaction);

                JVTransaction NepsePurchaseJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Nepse payable for purchase bill no. {purchaseBill.BillNumber}  ({descriptioinData})",
                    Credit = nepsePayable,
                    AccountHeadId = "7"
                };

                jvTransactions.Add(NepsePurchaseJvTransaction);

                JVTransaction CommissionIncomeJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Commission income for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData})",
                    Credit = CommissionAmount,
                    AccountHeadId = "3"
                };

                jvTransactions.Add(CommissionIncomeJvTransaction);

                JVTransaction DpJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"DP fee for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData})",
                    Credit = purchaseBill.DpAmount,
                    AccountHeadId = "8"
                };

                jvTransactions.Add(DpJvTransaction);

                JVTransaction SebonPayableJvTransaction = new JVTransaction()
                {
                    BranchId = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                    JournalVoucherId = journalVoucher.Id,
                    Description = $"Sebon regulatory fee payable for purchase bill no. {purchaseBill.BillNumber} ({descriptioinData}) ",
                    Credit = purchaseBill.SeboRegulatoryFee,
                    AccountHeadId = "5b"
                };

                jvTransactions.Add(SebonPayableJvTransaction);

                billStartCount += 1;
            }

            foreach (var clientCode in sellClientCodeList)
            {
                var clientSold = new List<SellFloorsheet>();
                var clientSells = new List<SellBillTransaction>();

                foreach (var data in sellFloorsheets)
                {
                    if (clientCode == data.ClientCode)
                    {
                        clientSold.Add(data);
                    }
                }

                SellBill sellBill = new SellBill()
                {
                    ClientCode = clientCode,
                    ClientName = clientSold[0].ClientName,
                    BillNumber = $"S/{sellStartCount}/{floorsheet.FiscalYear}",
                    BillDate = floorsheet.FloorsheetDateAd!,
                    ClearanceDate = floorsheet.ImportDateAd!,
                    FloorsheetId = floorsheet.Id,
                };

                foreach (var sell in clientSold)
                {
                    var InstrumentType = companies.Where(c => c.Symbol == sell.Symbol).Select(c => c.InstrumentType).FirstOrDefault()!;
                    SellBillTransaction sellBillTransaction = new SellBillTransaction()
                    {
                        TransactionNumber = sell.ContractNo,
                        Quantity = (int)sell.Quantity,
                        Rate = (decimal)sell.Rate,
                        CompanyName = companies.Where(c => c.Symbol == sell.Symbol).Select(c => c.Name).FirstOrDefault()!,
                        Symbol = sell.Symbol,
                        SellBillId = sellBill.Id
                    };
                    sellBillTransaction.CommissionRate = GetCommissionRate(InstrumentType,
                    sellBillTransaction.Amount);

                    sellBillTransaction.SeboCommission = Math.Round(((decimal)0.00015 * sellBillTransaction.Amount), 2);
                    sellBillTransaction.EffectiveRate = sellBillTransaction.Total / sellBillTransaction.Quantity;



                    sellBillTransactions.Add(sellBillTransaction);
                }
                sellBill.Transactions = sellBillTransactions.Where(t => t.SellBillId == sellBill.Id).ToList();
                sellBills.Add(sellBill);
                var CommissionAmount = sellBill.Transactions.Sum(s => s.CommissionAmount);
                sellBill.NepseCommission = Math.Round((0.2m * CommissionAmount), 2);
                sellBill.SeboRegulatoryFee = Math.Round((0.006m * CommissionAmount), 2);
                sellBill.BrokerCommission = Math.Round((0.794m * CommissionAmount), 2);
                sellBill.DpAmount = sellBill.Transactions.Select(e => e.CompanyName).Distinct().ToArray().Count() * 25;
                sellBill.SeboCommission = Math.Round((0.00015m * sellBill.ShareAmount), 2);
                sellStartCount += 1;
            }

            BuyFloorsheetWriter(buyFloorsheets, floorsheet.Id);
            SellFloorsheetWriter(sellFloorsheets, floorsheet.Id);
            PurchaseBillWriter(purchaseBills);
            PurchaseBillTransactionsWriter(purchaseBillTransactions);
            SellBillWriter(sellBills);
            SellBillTransactionsWriter(sellBillTransactions);
            JvWriter(journalVouchers);
            JvTransactionsWriter(jvTransactions);

            _transaction.Commit();
        }
        catch (Exception)
        {
            // var customException = new Exception(
            //          message: "Floorsheet Creation failed", ex
            //      );
            // customException.AddSentryTag("Floorsheet", "failed");
            // SentrySdk.CaptureException(customException);
            // throw new Exception(customException.Message);
            throw;
        }
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<Floorsheet> floorsheets)> GetFloorsheets(GetFloorsheetListQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM floorsheets";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql);
        int skip = (query.page - 1) * query.size;
        string sql = $"SELECT * FROM floorsheets OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
        var floorsheets = await _dbConnection.QueryAsync<Floorsheet>(sql);

        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, floorsheets.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<BuyFloorsheet> floorsheets)> GetBuyFloorsheets(GetBuyFloorsheetListQuery query)
    {

        string countSql = "SELECT COUNT(*) FROM buy_floorsheets  WHERE floorsheet_id = @FloorsheetId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { FloorsheetId = query.floorsheetId });
        int skip = (query.page - 1) * query.size;
        string sql = $"SELECT * FROM buy_floorsheets WHERE floorsheet_id = @FloorsheetId   OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY ";
        var buyFloorsheets = await _dbConnection.QueryAsync<BuyFloorsheet>(sql, new { FloorsheetId = query.floorsheetId });
        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);

        return (totalCount, pageSize, totalPages, currentPage, buyFloorsheets.ToList());
    }

    public async Task<(int totalCount, int pageSize, int totalPages, int currentPage, List<SellFloorsheet> floorsheets)> GetSellFloorsheets(GetSellFloorsheetListQuery query)
    {
        string countSql = "SELECT COUNT(*) FROM sell_floorsheets WHERE floorsheet_id = @FloorsheetId";
        int totalCount = await _dbConnection.ExecuteScalarAsync<int>(countSql, new { FloorsheetId = query.floorsheetId });
        int skip = (query.page - 1) * query.size;

        string sql = $"SELECT * FROM sell_floorsheets WHERE floorsheet_id = @FloorsheetId OFFSET {skip} ROWS FETCH NEXT {query.size} ROWS ONLY";
        var sellFloorsheets = await _dbConnection.QueryAsync<SellFloorsheet>(sql, new { FloorsheetId = query.floorsheetId });

        int pageSize = query.size;
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        int currentPage = Math.Min(query.page, totalPages);
        return (totalCount, pageSize, totalPages, currentPage, sellFloorsheets.ToList());
    }



}
