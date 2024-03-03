﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var saveUserViewModel = await _userService.GetByIdSaveViewModel(id);
            var editUserViewModel = _mapper.Map<EditUserViewModel>(saveUserViewModel);  

            return View("Index", editUserViewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(EditUserViewModel vm)
        //{
        //    if (!_validateUserSession.HasUser())
        //    {
        //        return RedirectToRoute(new { controller = "Login", action = "Index" });
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(vm);
        //    }


        //}
    }
}