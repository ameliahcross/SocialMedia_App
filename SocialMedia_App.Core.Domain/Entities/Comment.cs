using SocialMedia_App.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ParentCommentId { get; set; }

        // Foreign key
        public int PostId { get; set; }
        public int UserId { get; set; }

        // navigation properties
        public Post Post { get; set; }
        public User User { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; }
    }
}
