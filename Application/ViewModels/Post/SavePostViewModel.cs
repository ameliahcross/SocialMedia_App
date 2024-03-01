using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia_App.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public string YouTubeLink { get; set; }
    }
}
