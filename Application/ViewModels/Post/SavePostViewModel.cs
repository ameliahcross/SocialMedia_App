﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? PostImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
        public string YouTubeLink { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Content) 
                || PostImageUrl != null
                || !string.IsNullOrWhiteSpace(YouTubeLink);
        }

    }
}
