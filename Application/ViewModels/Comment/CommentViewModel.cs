
namespace SocialMedia_App.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int? ParentCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserImageUrl { get; set; }
        public List<CommentViewModel> Replies { get; set; }
    }
}
