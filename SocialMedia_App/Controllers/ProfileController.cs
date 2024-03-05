using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;
using System.Net.Http;

namespace SocialMedia_App.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IMapper _mapper;
        private readonly FileHelper _fileHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProfileController(IUserService userService, ValidateUserSession validateUserSession, 
            IMapper mapper , FileHelper fileHelper , IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var saveUserViewModel = await _userService.GetByIdSaveViewModel(id);
            var editUserViewModel = _mapper.Map<EditUserViewModel>(saveUserViewModel);  

            return View("Index", editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }
            var currentUser = await _userService.GetByIdSaveViewModel(vm.Id);
            var currentUserImageUrl = currentUser.ImageUrl;

            // source -> destination
            _mapper.Map(vm, currentUser);

            // encriptación de contraseña SI el usuario digitó una nueva
            if (!string.IsNullOrEmpty(vm.Password))
            {
                currentUser.Password = PasswordEncryption.ComputeSha256Hash(vm.Password);
            }
            // actualizar imagen SI el usuario decidió cambiarla
            if (vm.File != null && vm.File.Length > 0)
            {
                string imageUrl = _fileHelper.UploadFile(vm.File, currentUser.Id, true, currentUserImageUrl);
                currentUser.ImageUrl = imageUrl;
                await _userService.UpdateImage(vm.Id, imageUrl);
            }
            // Conservar la URL actual si no se proporciona un nuevo archivo
            var hey= currentUser.ImageUrl;
            
            await _userService.Update(_mapper.Map<SaveUserViewModel>(currentUser), vm.Id);
            TempData["ProfileModified"] = "Ha modificado los datos de su perfil.";

            var updatedViewModel = _mapper.Map<EditUserViewModel>(currentUser);
            return View("Index", updatedViewModel);
        }
    }
    
}
