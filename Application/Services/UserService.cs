using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository useRepository)
        {
            _repository = useRepository;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var usersList = await _repository.GetAllAsync();

            return usersList.Select(user => new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName,
                Role = user.Role,

            }).ToList();
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            SaveUserViewModel userViewModel = new();
            userViewModel.Id = user.Id;
            userViewModel.Username = user.Username;
            userViewModel.Password = user.Password;
            userViewModel.ConfirmPassword = user.Password;
            userViewModel.Email = user.Email;
            userViewModel.Name = user.Name;
            userViewModel.LastName = user.LastName;
            userViewModel.Role = user.Role;

            return userViewModel;
        }

        public async Task Update(SaveUserViewModel userToSave)
        {
            User user = new();
            user.Id = userToSave.Id;
            user.Username = userToSave.Username;
            user.Password = userToSave.Password;
            user.Email = userToSave.Email;
            user.Name = userToSave.Name;
            user.LastName = userToSave.LastName;
            user.Role = userToSave.Role;

            await _repository.UpdateAsync(user);
        }

        public async Task Add(SaveUserViewModel userToCreate)
        {
            var user = new User

            {
                Id = userToCreate.Id,
                Username = userToCreate.Username,
                Password = userToCreate.Password,
                Email = userToCreate.Email,
                Name = userToCreate.Name,
                LastName = userToCreate.LastName,
                Role = userToCreate.Role

            };

            await _repository.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(user);
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            User user = await _repository.LoginAsync(loginVm);

            if (user == null)
            {
                return null;
            }

            UserViewModel userVm = new();
            userVm.Id = user.Id;
            userVm.Name = user.Name;
            userVm.Email = user.Email;
            userVm.Username = user.Username;
            userVm.Password = user.Password;
            userVm.Role = user.Role;

            return userVm;
        }

        public async Task<bool> ValidateUsername(string username)
        {
            var existingUser = await _repository.GetByUsername(username);
            return existingUser == null;
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

    }
}

