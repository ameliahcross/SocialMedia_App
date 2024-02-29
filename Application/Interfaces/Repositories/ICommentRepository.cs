using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Interfaces.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllByPostIdWithIncludeAsync(int postId);
        Task<Comment> GetByIdWithIncludeAsync(int commentId);
    }
}
