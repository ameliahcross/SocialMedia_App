using SocialMedia_App.Core.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetFriendPostsByUserId(int userId);
        Task<IEnumerable<PostViewModel>> GetAllPostByUserIdWithIncludeAsync(int userId);
    }
}
