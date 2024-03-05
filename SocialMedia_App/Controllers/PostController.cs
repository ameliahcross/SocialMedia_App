using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Home;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;
        private readonly FileHelper _fileHelper;

        public PostController(IPostService postService, ValidateUserSession validateUserSession,
            IEmailService emailService, FileHelper fileHelper)
        {
            _postService = postService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
            _fileHelper = fileHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

     
    }
}
