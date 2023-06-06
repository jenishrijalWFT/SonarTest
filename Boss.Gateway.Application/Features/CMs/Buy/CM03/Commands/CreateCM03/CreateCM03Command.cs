using MediatR;
using Microsoft.AspNetCore.Http;
namespace Boss.Gateway.Application.Features.BuyBillCloseOut{
    public class CreateCM03Command : IRequest<CreateCM03CommandResponse>{
        public IFormFile? file { get; set; }

        public string FileName => file!.FileName;

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}