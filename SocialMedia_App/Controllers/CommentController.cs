using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Friendship;

namespace SocialMedia_App.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        } 

        [HttpPost]
        public async Task<IActionResult> Create(FriendshipHomeViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(returnUrl);
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
