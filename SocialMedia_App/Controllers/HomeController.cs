using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.ViewModels.Home;
using SocialMedia_App.Core.Application.ViewModels.Post;


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

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userVm = HttpContext.Session.Get<UserViewModel>("user");
            var posts = await _postService.GetAllPostByUserIdWithIncludeAsync(userVm.Id);

            var homeViewModel = new HomeViewModel
            {
                Posts = posts,
                NewPost = new SavePostViewModel()
            };

            return View(homeViewModel);
        }



    }
}
