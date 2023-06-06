using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.VoucherTypes;

public class CreateVoucherTypeCommandHandler : IRequestHandler<CreateVoucherTypeCommand, CreateVoucherTypeCommandResponse>
{

    private readonly IVoucherTypeRepository _voucherRepository;

    public CreateVoucherTypeCommandHandler(IVoucherTypeRepository voucherTypeRepository)
    {
        _voucherRepository = voucherTypeRepository;
    }

    public async Task<CreateVoucherTypeCommandResponse> Handle(CreateVoucherTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var sentryTransaction = SentrySdk.StartTransaction("create-voucher-type", "http-request");
            var response = new CreateVoucherTypeCommandResponse();
            var validator = new CreateVoucherTypeCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                    SentrySdk.CaptureException(new Exception("Invalid format " + string.Join(",", response.ValidationErrors)));
                }
            }

            if (response.Success)
            {
                var voucherType = new VoucherType() { Type = request.TypeName };
                await _voucherRepository.AddVoucherTypeName(voucherType);
                // TODO
                response.Message = $"Voucher Type {request.TypeName} Created Successfully";
            }
            sentryTransaction.Finish();

            return response;
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                 message: "Voucher Type Creation Command Failed", ex
             );
            customException.AddSentryTag("Create Voucher Type Command Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }
    }
}