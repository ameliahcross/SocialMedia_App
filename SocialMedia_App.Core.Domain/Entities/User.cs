using System;
using SocialMedia_App.Core.Domain.Common;

namespace SocialMedia_App.Core.Domain.Entities
{
    public class User : BaseEntity
	{
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string ActivationToken { get; set; }

        // navigation properties
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
    }
}

