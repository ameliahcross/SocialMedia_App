using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Domain.Entities;
namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<SaveCommentViewModel, CommentViewModel, Comment>
    {
        Task<List<CommentViewModel>> GetAllByPostId(int postId);
        Task<CommentViewModel> GetById(int commentId);
    }
}
