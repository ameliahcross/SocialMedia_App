using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Domain.Entities;
namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IFriendshipService : IGenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>
    {
        Task<List<FriendListViewModel>> GetFriendsByUserId(int userId);
        Task AddFriendshipAsync(int userId, int friendId);
        Task<bool> AreFriendsAsync(int userId, int friendId);
    }
}
