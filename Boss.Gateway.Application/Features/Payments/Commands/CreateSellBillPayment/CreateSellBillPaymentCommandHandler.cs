using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Application.Features.SellBills;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Payments
{
    public class CreateSellBillPaymentCommandList : IRequest<CreateSellBillPaymentCommandResponse>
    {
        public List<CreateSellBillPaymentCommand>? Commands { get; set; }
    }

    public class CreateSellBillPaymentCommandHandler : IRequestHandler<CreateSellBillPaymentCommandList, CreateSellBillPaymentCommandResponse>
    {
        private readonly ISellBillPaymentRepository _sellBillPaymentRepository;
        private readonly ISellBillRepository _sellBillRepository;

        public CreateSellBillPaymentCommandHandler(ISellBillPaymentRepository sellBillPaymentRepository, ISellBillRepository sellBillRepository)
        {
            _sellBillPaymentRepository = sellBillPaymentRepository;
            _sellBillRepository = sellBillRepository;
        }


        public async Task<CreateSellBillPaymentCommandResponse> Handle(List<CreateSellBillPaymentCommand> request, CancellationToken cancellationToken)
        {
            var sentryTransaction = SentrySdk.StartTransaction("added new sell bill payment", "http-request");

            try
            {
                var createSellBillPaymentCommandResponse = new CreateSellBillPaymentCommandResponse();

                var validator = new CreateSellBillPaymentCommandValidator();
                var validationTasks = request?.Select(command => validator.ValidateAsync(command));
                var validationResults = await Task.WhenAll(validationTasks!.ToArray());


                if (validationResults.Any(result => !result.IsValid))
                {
                    createSellBillPaymentCommandResponse.ValidationErrors = validationResults
    .SelectMany(result => result.Errors)
    .Select(error => error.ErrorMessage)
    .ToList();

                }

                if (createSellBillPaymentCommandResponse.Success)
                {
                    var query = new GetSellBillListQuery()
                    {
                        page = 1,
                        size = 10,
                        isBilled = false
                    };

                    var result = await _sellBillRepository.GetsellBills(query);
                    List<SellBill> sellBills = result.bills;

                    List<SellBillPayment> createSellBillPaymentCommands = sellBills
                        .SelectMany(sellBill => request!
                            .Select(requestItem => new SellBillPayment
                            {
                                Id = Guid.NewGuid(),
                                ClientName = sellBill.ClientCode,
                                ClientCode = sellBill.ClientCode,
                                BillNumber = sellBill.BillNumber,
                                DateInAd = sellBill.BillDate,
                                BillAmount = sellBill.NetPayableAmount,
                                CreatedAt = DateTime.Now,
                                AmountToPay = requestItem.AmountToPay,
                                PaymentMode = requestItem.PaymentMode,
                                ChequeNumber = requestItem.ChequeNumber,
                                ChequeDate = requestItem.ChequeDate,
                                PaymentTypeID = Guid.Parse("b172d4da-7531-4f25-8e1d-fb403ea21f33"),
                                PaidBranch = Guid.Parse("0108734c-347a-46b9-ab00-8d97c085f241"),
                            }))
                        .ToList();

                    await _sellBillPaymentRepository.CreateSellBillPayment(createSellBillPaymentCommands);
                    sentryTransaction.Finish();

                    return createSellBillPaymentCommandResponse;
                }

                return createSellBillPaymentCommandResponse;


            }
            catch (Exception ex)
            {
                var customException = new Exception(message: "Failed to add a new payment", ex);
                customException.AddSentryTag("Sell Bill Payment", "failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }

        public Task<CreateSellBillPaymentCommandResponse> Handle(CreateSellBillPaymentCommandList request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
