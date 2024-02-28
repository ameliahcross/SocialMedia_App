using Microsoft.Extensions.DependencyInjection;
using SocialMedia_App.Core.Application.Services;

namespace SocialMedia_App.Core.Application
{
    public static class ServicesRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
            #region "Services"
            //services.AddTransient<IAppointmentService, AppointmentService>();
            //services.AddTransient<IUserService, UserService>();
            #endregion            
        }

    }
}

 