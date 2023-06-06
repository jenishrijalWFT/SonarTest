public class FloorSheetQueries
{
    public static readonly String CreateFloorsheetEntryCommand = "INSERT INTO floorsheets (id, fiscal_year, floorsheet_date_ad, floorsheet_date_bs, import_date_ad, import_date_bs, amount, stock_commission, bank_deposit, created_at) VALUES (@Id, @FiscalYear, @FloorsheetDateAd, @FloorsheetDateBs, @ImportDateAd, @ImportDateBs, @Amount, @StockCommission, @BankDeposit, @CreatedAt)";

    public static readonly String BuyFloorsheetCopyCommand = "COPY buy_floorsheets (id, contract_no, symbol, buyer, seller, client_name, client_code, quantity, rate, stock_commission, bank_deposit,  floorsheet_id, created_at ) FROM STDIN (FORMAT BINARY)";


    public static readonly String SellFloorsheetCopyCommand = "COPY sell_floorsheets (id, contract_no, symbol, buyer, seller, client_name, client_code, quantity, rate, stock_commission, floorsheet_id , created_at ) FROM STDIN (FORMAT BINARY)";

    public static readonly String CopyPurchaseBillsCommand = "COPY purchase_bills (id, client_code, client_name, bill_number, bill_date, broker_commission, nepse_commission, sebo_commission, sebo_regulatory_fee, clearance_date, dp_amount, created_at)  FROM STDIN (FORMAT BINARY) ";

    public static readonly String CopyPurchaseBillTransactionsCommand = "COPY purchase_bill_transactions  (id, transaction_number, company_name, quantity, rate, commission_rate, sebo_commision, eff_rate, co_quantity, co_amount, purchase_bill_id, created_at ) FROM STDIN (FORMAT BINARY)";

}