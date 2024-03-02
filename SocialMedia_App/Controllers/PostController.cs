using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;

        public PostController(IPostService postService, ValidateUserSession validateUserSession, IEmailService emailService)
        {
            _postService = postService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel model)
        {
            if (!model.IsValid())
            {
                ModelState.AddModelError("", "No puedes crear publicaciones en blanco");
                return RedirectToAction("Index", "Home", model);
            }

            await _postService.Add(model);
            return RedirectToAction("Index", "Home");
        }

    }
}
