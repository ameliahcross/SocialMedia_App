using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class CommentController : Controller
    {
¿        private readonly ValidateUserSession _validateUserSession;
¿        private readonly ICommentService _commentService;

        public CommentController(ValidateUserSession validateUserSession, ICommentService commentService)
        {
            _validateUserSession = validateUserSession;
            _commentService = commentService;
        } 

        [HttpPost]
        public async Task<IActionResult> Create(FriendshipHomeViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Friendship");
            }
            await _commentService.Add(model.NewComment);

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return Redirect(returnUrl);
        }
    }
}
