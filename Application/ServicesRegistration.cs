﻿using Microsoft.Extensions.DependencyInjection;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.Services;
using System.Reflection;

namespace SocialMedia_App.Core.Application
{
    public static class ServicesRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
            #region "Services"
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<FileHelper>();

            #endregion

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}

 