using Microsoft.AspNetCore.Mvc;

namespace SocialMedia_App.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
