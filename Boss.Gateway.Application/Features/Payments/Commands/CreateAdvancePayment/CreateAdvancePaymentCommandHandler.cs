using MediatR;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;

namespace Boss.Gateway.Application.Features.Payments
{
    public class CreateAdvancePaymentCommandHandler : IRequestHandler<CreateAdvancePaymentCommand, CreateAdvancePaymentCommandResponse>
    {
        private readonly IAdvancePayment _advancePayment;

        public CreateAdvancePaymentCommandHandler(IAdvancePayment advancePayment)
        {
            _advancePayment = advancePayment;
        }
        public async Task<CreateAdvancePaymentCommandResponse> Handle(CreateAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateAdvancePaymentCommandResponse();
            var validator = new CreateAdvancePaymentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var advancePayment = new AdvancePayment
                {
                    Id = Guid.NewGuid(),
                    DateAd = request.DateAd,
                    DateBs = request.DateBs,
                    Branch = request.Branch,
                    ClientName = request.ClientName,
                    Amount = request.Amount,
                    PayentMode = request.PayentMode,
                    Bank = request.Bank,
                    ChequeNo = request.ChequeNo,
                    Remarks = request.Remarks,
                    CreatedBy = request.CreatedBy,
                };
                await _advancePayment.AddAdvancePayment(advancePayment);

                if (request.SelectedTransaction != null)
                {
                    advancePayment.AgainstSell = new List<AdvanceAgainstSell>();

                    foreach (var selectedTransactions in request.SelectedTransaction)
                    {
                        AdvanceAgainstSell againstSell = new AdvanceAgainstSell
                        {
                            TransactionNo = selectedTransactions.TransactionNo,
                            AdvanceAmount = advancePayment.Amount,
                            AdvancePaymentId = advancePayment.Id
                        };
                        advancePayment.AgainstSell!.Add(againstSell);
                        await _advancePayment.AddAgainstSell(againstSell);
                    }
                }
            }
            return response;
        }
    }
}
