using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Domain.Settings;
using SocialMedia_App.Infrastructure.Shared.Services;

namespace SocialMedia_App.Infrastructure.Shared
{
    public static class ServicesRegistration
	{
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }

    }
}

 