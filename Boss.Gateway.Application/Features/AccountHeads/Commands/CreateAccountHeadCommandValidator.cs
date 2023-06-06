using FluentValidation;

namespace Boss.Gateway.Application.Features.AccountHeads
{
    public class CreateAccountHeadCommandValidator : AbstractValidator<CreateAccountHeadCommand>
    {


       

        public CreateAccountHeadCommandValidator()
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
                .LessThanOrEqualTo(10 * 1024 * 1024) //10MB in bytes
                .WithMessage("File size is larger than allowed");
        }
    }
}
