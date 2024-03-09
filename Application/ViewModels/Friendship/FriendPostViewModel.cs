using SocialMedia_App.Core.Application.ViewModels.Comment;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
    public class FriendPostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string PostImageUrl { get; set; }
        public string YouTubeLink { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
