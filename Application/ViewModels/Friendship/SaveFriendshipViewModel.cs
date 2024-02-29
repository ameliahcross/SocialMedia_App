using System;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
	public class SaveFriendshipViewModel
	{
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}

