using MediatR;

namespace Boss.Gateway.Application.Features.Branches
{

    public class CreateBranchCommand : IRequest<CreateBranchCommandResponse>
    {
        public string? BranchCode { get; set; }
        public string? AccountCode { get; set; }

        public string? AccountName { get; set; }

        public string? PhoneNumber { get; set; }


        public override string? ToString()
        {
            return base.ToString();
        }
    }
}