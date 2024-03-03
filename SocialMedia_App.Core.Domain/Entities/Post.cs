using SocialMedia_App.Core.Domain.Common;

namespace SocialMedia_App.Core.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ImageUrl { get; set; }

        public string YouTubeLink { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
