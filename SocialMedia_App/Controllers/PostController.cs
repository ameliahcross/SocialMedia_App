using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
