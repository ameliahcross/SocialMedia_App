using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Core.Application.Helpers;
using AutoMapper;
using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Application.ViewModels.Comment;


namespace SocialMedia_App.Controllers
{
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public FriendshipController(ValidateUserSession validateUserSession, IUserService userService,
            IFriendshipService friendshipService, IMapper mapper, IPostService postService,
            ICommentService commentService)
        {
            _friendshipService = friendshipService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userVm = HttpContext.Session.Get<UserViewModel>("user");

            var friendPosts = await _postService.GetFriendPostsByUserId(userVm.Id); 
            var friendPostViewModels = _mapper.Map<List<FriendPostViewModel>>(friendPosts);

            foreach (var postViewModel in friendPostViewModels)
            {
                var postComments = await _commentService.GetAllByPostId(postViewModel.Id);
                postViewModel.Comments = _mapper.Map<List<CommentViewModel>>(postComments);
            }

            var friends = await _friendshipService.GetFriendsByUserId(userVm.Id); 
            var friendListViewModels = _mapper.Map<List<FriendListViewModel>>(friends);

            var friendshipHomeViewModel = new FriendshipHomeViewModel
            {
                Friends = friendListViewModels,
                FriendPosts = friendPostViewModels,
                NewComment = new SaveCommentViewModel(),
                NewFriend = new AddFriendViewModel(),
            };

            return View(friendshipHomeViewModel);
        }

        public async Task<IActionResult> AddFriend()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var viewModel = new FriendshipHomeViewModel
            {
                NewFriend = new AddFriendViewModel(),
                Friends = new List<FriendListViewModel>(), 
                FriendPosts = new List<FriendPostViewModel>(),
            };

            return View(viewModel);
        }

        // eliminar Friendship
        public async Task<IActionResult> Delete(int friendshipId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            await _friendshipService.Delete(friendshipId);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Search(FriendshipHomeViewModel model)
        {

            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var viewModel = new FriendshipHomeViewModel
            {
                NewFriend = model.NewFriend,
                Friends = new List<FriendListViewModel>(),
                FriendPosts = new List<FriendPostViewModel>(),
                User = new UserViewModel(),
                NewComment = new SaveCommentViewModel()
            };

            if (string.IsNullOrWhiteSpace(model.NewFriend.UserName))
            {
                ModelState.AddModelError("", "Debe proporcionar un nombre de usuario para la búsqueda.");
                return View("AddFriend", viewModel);
            }

            var userFound = await _userService.GetByUsername(model.NewFriend.UserName);
            if (userFound == null)
            {
                ViewBag.UserNotFound = "La búsqueda no trajo resultados.";
                return View("SearchResult", viewModel);
            }

            var userfounId = await _userService.GetByIdSaveViewModel(userFound.Id);
            var userSearching = HttpContext.Session.Get<UserViewModel>("user");
            bool alreadyFriends = await _friendshipService.AreFriendsAsync(userSearching.Id, userfounId.Id);
     
            if (userFound.Id == userSearching.Id)
            {
                ViewBag.FoundYourself = "Mi perfil";
            }

            if (alreadyFriends)
            {
                ViewBag.AlreadyFriends = "Ya es tu amigo(a)";
            }

            viewModel.User = userFound;
            return View("SearchResult", viewModel);
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(int friendId)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userSession = HttpContext.Session.Get<UserViewModel>("user");
            await _friendshipService.AddFriendshipAsync(userSession.Id, friendId);
            return RedirectToAction("Index");
        }


    }
}
