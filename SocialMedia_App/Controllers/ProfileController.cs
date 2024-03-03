using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, ValidateUserSession validateUserSession, IMapper mapper)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _mapper = mapper;
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
                return View("Index",vm);
            }
            var currentUser = await _userService.GetByIdSaveViewModel(vm.Id);
            _mapper.Map(vm, currentUser);

            // encriptación de contraseña SI el usuario digitó una nueva
            if (!string.IsNullOrEmpty(vm.Password))
            {
                currentUser.Password = PasswordEncryption.ComputeSha256Hash(vm.Password);
            }

            await _userService.Update(_mapper.Map<SaveUserViewModel>(currentUser), vm.Id);
            TempData["ProfileModified"] = "Ha modificado los datos de su perfil.";
            var updatedViewModel = _mapper.Map<EditUserViewModel>(currentUser);
            return View("Index", updatedViewModel);
        }
    }
}
