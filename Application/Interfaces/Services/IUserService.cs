using System;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Core.Application.ViewModels.Login;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<bool> ValidateUsername(string username);
        Task<UserViewModel> GetUserWithFriendsAndPostsAsync(int userId);
    }
}

