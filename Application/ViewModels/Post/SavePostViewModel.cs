using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia_App.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public string? PostImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

        public string? YouTubeLink { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
