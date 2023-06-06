using AutoMapper;
using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Branches
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreateBranchCommandResponse>
    {

        private readonly IBranchRepository _branchRepository;




        public CreateBranchCommandHandler(IBranchRepository branchRepository, IMapper mapper, IMediator mediator)
        {
            _branchRepository = branchRepository;
        }


        public async Task<CreateBranchCommandResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var sentryTransaction = SentrySdk.StartTransaction("create new branch", "http-request");
            try
            {
                var createBranchCommandResponse = new CreateBranchCommandResponse();
                var validator = new CreateBranchCommandValidator(_branchRepository);
                var validationResult = await validator.ValidateAsync(request);
                if (validationResult.Errors.Count > 0)
                {
                    createBranchCommandResponse.Success = false;
                    createBranchCommandResponse.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        createBranchCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                if (createBranchCommandResponse.Success)
                {

                    var branch = new Branch() { BranchCode = request.BranchCode, AccountCode = request.AccountCode, AccountName = request.AccountName, PhoneNumber = request.PhoneNumber };
                    await _branchRepository.AddBranch(branch);

                    createBranchCommandResponse.Message = "New branch created successfully";
                    sentryTransaction.Finish();

                }
                return createBranchCommandResponse;
            }
            catch (Exception ex)
            {
                var customException = new Exception(message: "Failed to create a new branch", ex);
                customException.AddSentryTag("Branch", "failed");
                SentrySdk.CaptureException(customException);
                throw new Exception(ex.Message);
            }
        }
    }
}