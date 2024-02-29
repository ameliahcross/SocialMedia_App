using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Infrastructure.Persistence.Repositories
{
    public interface IFriendshipRepository : IGenericRepository<Friendship>
    {
        Task<List<Friendship>> GetAllFriendsByUserIdAsync(int userId);
        Task DeleteFriendshipAsync(int userId, int friendId);

    }
}