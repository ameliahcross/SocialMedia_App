using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    public class FriendshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
