using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly FileHelper _fileHelper;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CommentController(IPostService postService, ValidateUserSession validateUserSession,
            FileHelper fileHelper, IMapper mapper, ICommentService commentService)
        {
            _postService = postService;
            _validateUserSession = validateUserSession;
            _fileHelper = fileHelper;
            _mapper = mapper;
            _commentService = commentService;
        } 

        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCommentViewModel newComment)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Friendship");
            }

            await _commentService.Add(newComment);

            return RedirectToRoute(new { controller = "Friendship", action = "Index" });
        }
    }
}
