using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
    public class FriendshipHomeViewModel
    {
        public List<FriendListViewModel> Friends { get; set; }
        public UserViewModel User { get; set; }
        public List<FriendPostViewModel> FriendPosts { get; set; }
        public AddFriendViewModel NewFriend { get; set; }
        public SaveCommentViewModel NewComment { get; set; }
    }
}
