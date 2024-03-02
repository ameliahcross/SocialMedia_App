using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Models;
using System.Diagnostics;

namespace SocialMedia_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;

        public HomeController(IPostService postService, ValidateUserSession validateUserSession, IEmailService emailService)
        {
            _postService = postService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Register" });
            }

            return View();
        }


    }
}
