using Microsoft.EntityFrameworkCore;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Contexts;

namespace SocialMedia_App.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationContext _dbContext;
        public PostRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // para listar las publicaciones de un usuario (el de la sesión iniciada)
        public async Task<List<Post>> GetAllPostByUserIdWithIncludeAsync(int userId)
        {
            return await _dbContext.Set<Post>()
                .Where(post => post.UserId == userId)
                .Include(post => post.User)
                .Include(post => post.Comments)
                .OrderByDescending(post => post.CreationDate)
                .ToListAsync();
        }

        // para listar las publicaciones de diferentes usuarios (los amigos del usuario)
        public async Task<List<Post>> GetPostsByUsersIdsWithIncludeAsync(List<int> userIds)
        {
            return await _dbContext.Set<Post>()
                .Where(post => userIds.Contains(post.UserId))
                .Include(post => post.User)
                .OrderByDescending(post => post.CreationDate)
                .ToListAsync();
        }



    }
}
