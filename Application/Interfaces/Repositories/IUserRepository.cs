using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Core.Application.ViewModels.Login;

namespace SocialMedia_App.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel loginVm);
        Task<User> GetByUsername(string username);

        Task<User> GetUserWithIncludeById(int userId);
    }
}

