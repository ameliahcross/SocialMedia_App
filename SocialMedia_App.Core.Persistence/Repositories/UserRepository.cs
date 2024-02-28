﻿using System;
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

        public override async Task AddAsync(User user)
        {
            user.Password = PasswordEncryption.ComputeSha256Hash(user.Password);
            // debo colocar el "await" antes de llamar a la clase padre y ejecutar su method
            await base.AddAsync(user);
        }

        public async Task<User> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryption.ComputeSha256Hash(loginVm.Password);
            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Username == loginVm.Username && user.Password == passwordEncrypt);
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
        }
    }
}

