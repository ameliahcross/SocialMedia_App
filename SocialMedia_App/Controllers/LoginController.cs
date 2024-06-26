﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Login;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Middlewares;

namespace SocialMedia_App.Controllers
{
    public class LoginController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IUserService _userService;

        public LoginController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        // muestra formulario de login si no hay sesión
        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // recibe el formulario de login y devuelve vista dependiendo de su estado
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            UserViewModel userVm = await _userService.Login(vm);

            if (userVm == null)
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos");
                return View(vm);
            }

            if (!userVm.IsActive)
            {
                ModelState.AddModelError("userValidation", "La cuenta del usuario no está activa. Por favor, revise su correo electrónico para activarla.");
                return View(vm);
            }

            // guarda la información del usuario loggueado
            HttpContext.Session.Set<UserViewModel>("user", userVm);
            return RedirectToAction("Index", "Home");
        }

        // logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Login");
        }


    }
}
