using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<SavePostViewModel, PostViewModel,Post>
    {
        Task<List<PostViewModel>> GetFriendPostsByUserId(int userId);
        Task<List<PostViewModel>> GetAllPostByUserIdWithIncludeAsync(int userId);
        Task PostImage(int postId, string imageUrl);
    }
}
