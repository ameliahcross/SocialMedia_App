using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;
using System.Threading.Tasks;
namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IFriendshipService : IGenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>
    {
        Task<bool> AreFriendsAsync(int userId, int friendId);
        Task<List<FriendListViewModel>> GetFriendsByUserId(int userId);
        Task AddFriendshipAsync(int userId, int friendId);
    }
}
