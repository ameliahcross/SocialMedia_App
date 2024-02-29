using SocialMedia_App.Core.Domain.Common;
namespace SocialMedia_App.Core.Domain.Entities
{
    public class Friendship : BaseEntity
    {
        // Foreign keys
        public int UserId { get; set; }
        public int FriendId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public User Friend { get; set; }
    }
}
