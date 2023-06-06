using Boss.Gateway.Application.Contracts.Persistence;
using Boss.Gateway.Domain.Entities;
using MediatR;
using Sentry;

namespace Boss.Gateway.Application.Features.Companies;


public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyCommandResponse>
{
    private readonly ICompanyRepository _CompanyRepository;
    public CreateCompanyCommandHandler(ICompanyRepository CompanyRepository)
    {
        _CompanyRepository = CompanyRepository;
    }

    public async Task<CreateCompanyCommandResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sentryTransaction = SentrySdk.StartTransaction("create-company", "http-request");
            var createCompanyCommandResponse = new CreateCompanyCommandResponse();
            var validator = new CreateCompanyCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
            {
                createCompanyCommandResponse.Success = false;
                createCompanyCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCompanyCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (createCompanyCommandResponse.Success)
            {
                var company = new Company() { Name = request.Name, Symbol = request.Symbol, Email = request.Email ?? "", Website = request.Website ?? "", InstrumentType = request.InstrumentType };

                company = await _CompanyRepository.AddCompanyAsync(company);
                createCompanyCommandResponse.Message = "New Company added successfully";
            }

            return createCompanyCommandResponse;
        }
        catch (Exception ex)
        {
            var customException = new Exception(
                  message: "Companny Creation Command Failed", ex
              );
            customException.AddSentryTag("Create Company Command Handler", "Failed");
            SentrySdk.CaptureException(customException);
            throw new Exception(ex.Message);
        }
    }
}