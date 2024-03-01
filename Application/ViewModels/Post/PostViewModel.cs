using SocialMedia_App.Core.Application.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Image { get; set; }
        public string YouTubeLink { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
