using MediatR;
using Microsoft.AspNetCore.Http;

namespace Boss.Gateway.Application.Features.FloorSheets
{

    public class CreateFloorSheetCommand : IRequest<CreateFloorSheetCommandResponse>
    {

        public IFormFile? file { get; set; }

        public string FileName => file!.FileName;

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}