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
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName

            }).ToList();
        }

        // para registrar un nuevo usuario y enviar email de confirmacion
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel userToAdd)
        {
            var activationToken = Guid.NewGuid().ToString();
            userToAdd.ActivationToken = activationToken;
            userToAdd.IsActive = false;

            SaveUserViewModel saveUserViewModel = await base.Add(userToAdd);
            var activationUrl = $"http://localhost:7145/User/Activate?token={activationToken}";

            await _emailService.SendAsync(new EmailRequest
            {   
                To = saveUserViewModel.Email,
                From = _emailService.MailSettings.EmailFrom,
                Body = $"Hola, {saveUserViewModel.UserName}, por favor activa tu cuenta haciendo click aquí: {activationUrl}",
                Subject = "Bienvenido(a). Activa tu cuenta en My Social Media."
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

        // para validar la existencia de un usuario
        public async Task<bool> UserExists(string username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            return existingUser != null;
        }

        // recuperar por nombre de usuario
        public async Task<UserViewModel> GetByUsername(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserViewModel>(user);
        }

        // para generar una contraseña
        public string GenerateSecurePassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }
            return new string(chars);
        }

        // para reestablecer la contraseña
        public async Task<bool> UpdatePassword(string username, string newPassword)
        {
            return await _userRepository.UpdatePasswordAsync(username, newPassword);
        }

        // para obtener usuario por token
        public async Task<UserViewModel> GetUserByActivationToken(string token)
        {
            var user = await _userRepository.GetUserByActivationTokenAsync(token);
            if (user != null)
            {
                return _mapper.Map<UserViewModel>(user);
            }
            return null;
        }

        // para activar usuario
        public async Task ActivateUser(string activationToken)
        {
            var user = await _userRepository.GetUserByActivationTokenAsync(activationToken);
            if (user != null)
            {
                user.IsActive = true;
                user.ActivationToken = null;
                await _userRepository.UpdateAsync(user, user.Id);
            }
        }


    }
}

