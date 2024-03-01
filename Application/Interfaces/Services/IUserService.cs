using System;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Core.Application.ViewModels.Login;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<bool> UserExists(string username);
        Task<bool> UpdatePassword(string username, string newPassword);
        Task<UserViewModel> GetByUsername(string username);
        string GenerateSecurePassword();

        //Task<UserViewModel> GetUserWithFriendsAndPostsAsync(int userId);
    }
}

