using Boss.Gateway.Application.Models.Email;

namespace Boss.Gateway.Application.Contracts.Infrastructure
{

    public interface IEmailService {
        Task<bool> SendEmail(Email email);
    }
}