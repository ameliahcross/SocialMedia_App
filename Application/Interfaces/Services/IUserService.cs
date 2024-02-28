using System;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Core.Application.ViewModels.User;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserViewModel, SaveUserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
        Task<bool> ValidateUsername(string username);
        Task<User> GetById(int id);
    }
}

