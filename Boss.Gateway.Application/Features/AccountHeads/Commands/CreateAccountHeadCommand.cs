using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boss.Gateway.Application.Features.AccountHeads
{

    public class CreateAccountHeadCommand : IRequest<CreateAccountHeadCommandResponse>
    {
        
        public IFormFile? file { get; set; }

        public string FileName => file!.FileName;

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}