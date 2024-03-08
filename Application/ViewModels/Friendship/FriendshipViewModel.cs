using System;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
	public class FriendshipViewModel
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserImageUrl { get; set; }
        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendLastName { get; set; }
        public string FriendImageUrl { get; set; }
    }
}

