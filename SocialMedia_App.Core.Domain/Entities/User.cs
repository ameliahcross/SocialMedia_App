using System;
using SocialMedia_App.Core.Domain.Common;

namespace SocialMedia_App.Core.Domain.Entities
{
    public class User : BaseEntity
	{
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
         
        public string Name { get; set; }

        public string LastName { get; set; }


    }
}

