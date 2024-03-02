using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia_App.Core.Application.ViewModels.Post;

namespace SocialMedia_App.Core.Application.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public SavePostViewModel NewPost { get; set; }

        public HomeViewModel()
        {
                Posts = new List<PostViewModel>();
        }
    }
}
