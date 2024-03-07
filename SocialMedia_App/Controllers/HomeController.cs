using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.ViewModels.Home;
using SocialMedia_App.Core.Application.ViewModels.Post;
using System.Net.Http;
using System.Web;
using SocialMedia_App.Core.Application.Services;
using SocialMedia_App.Core.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Hosting;


namespace SocialMedia_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;
        private readonly FileHelper _fileHelper;
        private readonly IMapper _mapper;

        public HomeController(IPostService postService, ValidateUserSession validateUserSession,
            FileHelper fileHelper, IUserService userService, IMapper mapper)
        {
            _postService = postService;
            _validateUserSession = validateUserSession;
            _fileHelper = fileHelper;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userVm = HttpContext.Session.Get<UserViewModel>("user");
            var posts = await _postService.GetAllPostByUserIdWithIncludeAsync(userVm.Id);

            var homeViewModel = new HomeViewModel
            {
                Posts = posts,
                NewPost = new SavePostViewModel()
            };

            return View(homeViewModel);
        }

        // creación de posts

        [HttpPost]
        public async Task<IActionResult> Create(HomeViewModel model)
        {
            if (!ModelState.IsValid || !model.IsValid())
            {
                ModelState.AddModelError("", "No puedes crear publicaciones en blanco");
                return View("Index", model);
            }

            var userSession = HttpContext.Session.Get<UserViewModel>("user");
            model.NewPost.UserId = userSession.Id;

            if (!string.IsNullOrWhiteSpace(model.NewPost.YouTubeLink))
            {
                model.NewPost.YouTubeLink = ConvertToEmbedUrl(model.NewPost.YouTubeLink);
            }

           SavePostViewModel postVm = await _postService.Add(model.NewPost);

            if (model.NewPost.File != null && postVm.Id != 0)
            {
                string imageUrl = _fileHelper.UploadFile(model.NewPost.File, postVm.Id, "Posts");
                await _postService.PostImage(postVm.Id, imageUrl);
                postVm.PostImageUrl = imageUrl;
            }
            
            return RedirectToAction("Index");
        }

        public string ConvertToEmbedUrl(string videoUrl)
        {
            var videoId = "";
            var videoUri = new Uri(videoUrl);

            if (videoUri.Host.Contains("youtube.com"))
            {
                var query = HttpUtility.ParseQueryString(videoUri.Query);
                videoId = query["v"];
            }
            else if (videoUri.Host.Contains("youtu.be"))
            {
                videoId = videoUri.AbsolutePath.Trim('/');
            }
            return $"https://www.youtube.com/embed/{videoId}";
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            await _postService.Delete(id);
           return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var post = await _postService.GetByIdSaveViewModel(id);

            EditPostViewModel toEdit = new();
            toEdit.Id = post.Id;    
            toEdit.UserId = post.UserId;
            toEdit.Content = post.Content;
            toEdit.YouTubeLink = post.YouTubeLink;
            toEdit.PostImageUrl = post.PostImageUrl;

            return View(toEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!string.IsNullOrWhiteSpace(vm.YouTubeLink))
            {
                vm.YouTubeLink = ConvertToEmbedUrl(vm.YouTubeLink);
            }

            var currentPost = await _postService.GetByIdSaveViewModel(vm.Id);
            _mapper.Map(vm, currentPost);

            await _postService.Update(currentPost, vm.Id);

            if (!string.IsNullOrWhiteSpace(vm.PostImageUrl))
            {
                currentPost.PostImageUrl = vm.PostImageUrl;
            }

            if (vm.File != null && vm.File.Length > 0)
            {
                string imageUrl = _fileHelper.UploadFile(vm.File, currentPost.Id, "Posts");
                await _postService.PostImage(currentPost.Id, imageUrl);
            }
            //Pero en el caso de que se esté editando un post que ya viene con una imagen originalmente,
            //si edito un campo que no sea el de la imagen, o sea, si no subo ningun archivo, no me está
            //conservando la imagen previa sino que se esta guardando el registro con la propiedad en null
            return RedirectToAction("Index");
        }




    }
}
