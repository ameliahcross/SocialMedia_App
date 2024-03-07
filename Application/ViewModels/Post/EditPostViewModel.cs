using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Post
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public string? Content { get; set; }
        public string? YouTubeLink { get; set; }
        public string? PostImageUrl { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        public IFormFile? File { get; set; }
    }
}
