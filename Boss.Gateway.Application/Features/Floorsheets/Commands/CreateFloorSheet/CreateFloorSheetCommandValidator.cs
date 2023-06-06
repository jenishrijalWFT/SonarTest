using FluentValidation;

namespace Boss.Gateway.Application.Features.FloorSheets
{
    public class CreateFloorSheetCommandValidator : AbstractValidator<CreateFloorSheetCommand>
    {


       

        public CreateFloorSheetCommandValidator()
        {
            

          RuleFor(file => file.FileName)
            .NotEmpty()
            .WithMessage("File name is required")
            .Must(fileName => 
                fileName.EndsWith(".xls") || 
                fileName.EndsWith(".xlsx") || 
                fileName.EndsWith(".csv"))
            .WithMessage("File type must be .xls, .xlsx, or .csv");

            RuleFor(x => x.file!.Length)
                .NotNull()
                .LessThanOrEqualTo(3 * 1024 * 1024) //3MB in bytes
                .WithMessage("File size is larger than allowed");
        }
    }
}
