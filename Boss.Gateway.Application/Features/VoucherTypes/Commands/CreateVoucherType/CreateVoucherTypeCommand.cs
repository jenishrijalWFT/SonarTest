using MediatR;

namespace Boss.Gateway.Application.Features.VoucherTypes;

public class CreateVoucherTypeCommand : IRequest<CreateVoucherTypeCommandResponse>
{

    public string TypeName { get; set; } = "";
}