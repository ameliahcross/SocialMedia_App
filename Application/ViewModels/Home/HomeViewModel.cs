using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Application.ViewModels.Post;

namespace SocialMedia_App.Core.Application.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public SavePostViewModel NewPost { get; set; }
        public SaveCommentViewModel? NewComment { get; set; }

        public HomeViewModel()
        {
            Posts = new List<PostViewModel>();
            Comments = new List<CommentViewModel>();
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(NewPost.Content)
                || (NewPost.File != null
                || !string.IsNullOrWhiteSpace((NewPost.YouTubeLink)));
        }
    }
}
