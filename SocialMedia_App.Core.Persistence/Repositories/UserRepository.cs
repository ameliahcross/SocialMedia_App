using System;
using Microsoft.EntityFrameworkCore;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.ViewModels.Login;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Contexts;

namespace SocialMedia_App.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User user)
        {
            user.Password = PasswordEncryption.ComputeSha256Hash(user.Password);
            return await base.AddAsync(user);
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<bool> UpdatePasswordAsync(string username, string newPassword)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.Password = PasswordEncryption.ComputeSha256Hash(newPassword);
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

