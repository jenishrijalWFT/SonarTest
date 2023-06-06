
using Boss.Gateway.Application.Contracts.Persistence;
using FluentValidation;

namespace Boss.Gateway.Application.Features.Branches
{
    public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
    {
        private readonly IBranchRepository _branchRepository;
        public CreateBranchCommandValidator(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
            RuleFor(p => p.BranchCode).NotEmpty().WithMessage("{PropertyName} is required").NotNull();

            // RuleFor(e => e).MustAsync(BranchCodeUnique).WithMessage("Branch code already exists.");

            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("{PropertyName} is required").NotNull().MinimumLength(6);

        }

        // private async Task<bool> BranchCodeUnique(CreateBranchCommand e, CancellationToken token) {
        //     return !(await _branchRepository.IsBranchCodeUnique(e.BranchCode!));
        // }
    }
}