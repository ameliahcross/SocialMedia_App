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
        public FriendshipController(ValidateUserSession validateUserSession, IUserService userService,
            IFriendshipService friendshipService, IMapper mapper, IPostService postService)
        {
            _friendshipService = friendshipService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
            _postService = postService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userVm = HttpContext.Session.Get<UserViewModel>("user");

            var friendPosts = await _postService.GetFriendPostsByUserId(userVm.Id); // lista de posts de amigos
            var friendPostViewModels = _mapper.Map<List<FriendPostViewModel>>(friendPosts);

            var friends = await _friendshipService.GetFriendsByUserId(userVm.Id); // lista de amigos
            var friendListViewModels = _mapper.Map<List<FriendListViewModel>>(friends);

            var friendshipHomeViewModel = new FriendshipHomeViewModel
            {
                Friends = friendListViewModels,
                FriendPosts = friendPostViewModels,
                NewFriend = new AddFriendViewModel()
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

        [HttpPost]
        public async Task<IActionResult> Search(FriendshipHomeViewModel model)
        { 
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
            var nameSearched = userFound;

            if (userFound == null)
            {
                ViewBag.UserNotFound = "La búsqueda no trajo resultados.";
                return View("SearchResult", viewModel);
            }

            viewModel.User = userFound;
            return View("SearchResult", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int friendId)
        {
            var userSession = HttpContext.Session.Get<UserViewModel>("user");

            SaveFriendshipViewModel newFriendship = new SaveFriendshipViewModel
            {
                UserId = userSession.Id,
                FriendId = friendId
            };

            var addingFriend= await _friendshipService.Add(newFriendship);

            if (addingFriend != null)
            {
                TempData["SuccessMessage"] = "¡Amigo agregado exitosamente!";
            }
            else
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar agregar al amigo.";
            }
            return RedirectToAction("Index");
        }








    }
}
