

namespace SocialMedia_App.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        //public int Id { get; set; }
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
