﻿using System;
using Microsoft.AspNetCore.Http;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.ViewModels.User
{
	public class UserViewModel
	{
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}

