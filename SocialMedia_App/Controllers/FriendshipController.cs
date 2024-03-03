using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.Services;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ValidateUserSession _validateUserSession;
        public FriendshipController(ValidateUserSession validateUserSession, IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }
    }
}
