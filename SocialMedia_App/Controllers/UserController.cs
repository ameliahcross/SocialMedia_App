using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Login;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Core.Application.DTOs.Email;

//registro de usuarios, restablecimiento de contraseñas activación de cuentas
//ademas de la edición del perfil de usuario

namespace WebApp.SocialMedia_App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;

        public UserController(IUserService userService,ValidateUserSession validateUserSession, IEmailService emailService)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
        }

        public async Task<IActionResult> Activate(string token)
        {
            if (!_validateUserSession.HasUser())
            {
                return View("Register");
            }

            var userToActivate = await _userService.GetUserByActivationToken(token);

            if (userToActivate != null)
            {
                await _userService.ActivateUser(token);
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                return View("Register");
            }
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            var isUsernameValid = await _userService.UserExists(vm.UserName);
            if (!isUsernameValid)
            {
                ModelState.AddModelError("UserName", "El nombre de usuario ya existe.");
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        public IActionResult ResetPassword()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            bool userExists = await _userService.UserExists(vm.Username);
            if (userExists)
            {
                var newPassword = _userService.GenerateSecurePassword(8);

                UserViewModel userVm = await _userService.GetByUsername(vm.Username);
                await _userService.UpdatePassword(userVm.UserName, newPassword);

                await _emailService.SendAsync(new EmailRequest
                {
                    To = userVm.Email,
                    Subject = "Restablecimiento de Contraseña",
                    Body = $"Su nueva contraseña es: {newPassword}"
                });

                TempData["ResetPasswordSuccess"] = "Se ha enviado la nueva contraseña a su correo electrónico.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("userValidation", "El nombre de usuario no existe.");
                return View(vm);
            }
        }

    }
}
