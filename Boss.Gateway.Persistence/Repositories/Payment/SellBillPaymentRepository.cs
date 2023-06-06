using Boss.Gateway.Application.Contracts.Persistence;
using System.Data;
using Dapper;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Persistence.Repositories
{
    public class SellBillPaymentRepository : ISellBillPaymentRepository
    {
        private readonly IDbConnection _dbConnection;

        public SellBillPaymentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        }

        public async Task CreateSellBillPayment(List<SellBillPayment> createSellBillPaymentCommands)
        {

            var sql = @"INSERT INTO sell_bill_payment
                (id, client_name, ledger_amount, bill_number, bill_amount, paid_amount, pending_amount, close_out_amount, amount_to_pay, cheque_number, cheque_date, paid_branch, date_in_ad, date_in_bs, created_at, created_by, status, payment_mode, is_billed, payment_type_id)
                VALUES
                (@Id, @ClientName, @LedgerAmount, @BillNumber, @BillAmount, @PaidAmount, @PendingAmount, @CloseOutAmount, @AmountToPay, @ChequeNumber, @ChequeDate, @PaidBranch, @DateInAD, @DateInBS, @CreatedAt, @CreatedBy, @Status, @PaymentMode, @IsBilled, @PaymentTypeId)";

            await _dbConnection.ExecuteAsync(sql, createSellBillPaymentCommands.Select(command => new
            {
                command.Id,
                command.ClientName,
                command.LedgerAmount, // Set the appropriate values
                command.BillNumber,
                command.BillAmount,
                PaidAmount = 0, // Set the appropriate values
                PendingAmount = 0, // Set the appropriate values
                command.CloseOutAmount, // Set the appropriate values
                command.AmountToPay,
                command.ChequeNumber,
                command.ChequeDate,
                command.DateInAd,
                command.PaidBranch,
                command.DateInBS, // Set the appropriate values
                command.CreatedAt,
                command.CreatedBy,
                command.PaymentMode,
                command.status,// Set the appropriate values
                command.isBilled,
                command.PaymentTypeID, // Set the appropriate values
            }));
        }


    }
}
