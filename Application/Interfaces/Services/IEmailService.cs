using SocialMedia_App.Core.Application.DTOs.Email;
using SocialMedia_App.Core.Domain.Settings;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
