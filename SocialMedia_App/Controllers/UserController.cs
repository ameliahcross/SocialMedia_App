using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Middlewares;
using SocialMedia_App.Core.Application.DTOs.Email;

//registro de usuarios, restablecimiento de contraseñas activación de cuentas

namespace WebApp.SocialMedia_App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IEmailService _emailService;
        private readonly FileHelper _fileHelper;

        public UserController(IUserService userService,ValidateUserSession validateUserSession,
            IEmailService emailService, FileHelper fileHelper)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _emailService = emailService;
            _fileHelper = fileHelper;   
        }

        public async Task<IActionResult> Activate(string token)
        {
            if (!_validateUserSession.HasUser())
            {
                TempData["NoAccess"] = "No tiene permiso para acceder a esta página. Primero debe iniciar sesión.";
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var userToActivate = await _userService.GetUserByActivationToken(token);

            if (userToActivate != null)
            {
                await _userService.ActivateUser(token);
                TempData["SuccessMessage"] = "Su cuenta ha sido activada exitosamente. Ahora puede iniciar sesión.";
                return RedirectToAction("Index", "Login");
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
                return View(vm);
            }

            var isUsernameTaken = await _userService.UserExists(vm.UserName);
            if (isUsernameTaken)
            {
                ModelState.AddModelError("UserName", "El nombre de usuario ya existe.");
                return View(vm);
            }
            
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveUserViewModel userVm = await _userService.Add(vm);

            if (vm.File != null && userVm.Id != 0)
            {
                string imageUrl = _fileHelper.UploadFile(vm.File, userVm.Id, "Users");
                await _userService.UpdateImage(userVm.Id, imageUrl);
            }

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
        public async Task<IActionResult> ResetPassword(ResetPassViewModel vm)
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

                TempData["PasswordReset"] = "Inicie sesión con la nueva contraseña enviada a su correo electrónico";
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
