using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Friendship
{
    public class FriendListViewModel
    {
        public int FriendId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
