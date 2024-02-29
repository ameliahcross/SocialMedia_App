using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetAllPostByUserIdWithIncludeAsync(int userId);
        Task<List<Post>> GetPostsByUsersIdsWithIncludeAsync(List<int> userIds);
    }
}
