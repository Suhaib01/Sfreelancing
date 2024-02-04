using VUA.Core.Models;

namespace VUA.EF.Repositories.Service
{
    public interface IEmailService
    {
        Task SendtestEmail(UserEmailOptions useremailOptions);
        Task SendEmailForConfirmation(UserEmailOptions useremailOptions);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}