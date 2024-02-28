using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Application.Repositories;

namespace SocialMedia_App.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
        Task<User> GetByUsername(string username);
    }
}

