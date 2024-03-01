using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia_App.Core.Application.DTOs.Email;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Login;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper) : base(userRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var usersList = await _userRepository.GetAllAsync();

            return usersList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                //Role = user.Role,

            }).ToList();
        }

        // para registrar un nuevo usuario y enviar email de confirmacion
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel userToAdd)
        {
            var activationToken = Guid.NewGuid().ToString();
            userToAdd.ActivationToken = activationToken;

            SaveUserViewModel saveUserViewModel = await base.Add(userToAdd);

            await _emailService.SendAsync(new EmailRequest
            {
                To = saveUserViewModel.Email,
                From = _emailService.MailSettings.EmailFrom,
                Body = $"El usuario: {saveUserViewModel.UserName} se ha creado exitosamente!",
                Subject = "Bienvenido(a) a My Social Media."
            });

            return saveUserViewModel;
        }

        // para iniciar sesion
        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _userRepository.LoginAsync(loginVm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userVm = _mapper.Map<UserViewModel>(user);
            return userVm;
        }

        // para validar que no se registren con un nombre de usuario existente
        public async Task<bool> ValidateUsername(string username)
        {
            var existingUser = await _userRepository.GetByUsername(username);
            return existingUser == null;
        }

        //public async Task<List<UserViewModel>> GetAllViewModelWithInclude()
        //{
        //    var userList = await _userRepository.GetAllWithIncludeAsync(new List<string> { "Friendship" });

        //    return userList.Select(user => new UserViewModel
        //    {
        //        Name = user.Name,
        //        Username = user.UserName,
        //        Id = user.Id,
        //        Email = user.Email,
        //        Password = user.Password,
        //        Phone = user.Phone
        //    }).ToList();
        //}
    }
}

