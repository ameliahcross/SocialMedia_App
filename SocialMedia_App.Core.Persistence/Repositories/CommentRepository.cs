using Microsoft.EntityFrameworkCore;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Contexts;

namespace SocialMedia_App.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationContext _dbContext;
        public CommentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // para mostrar todos los comentarios de un post especifico incluyendo sus replies
        public async Task<List<Comment>> GetAllByPostIdWithIncludeAsync(int postId)
        {
            return await _dbContext.Set<Comment>()
               .Where(comment => comment.PostId == postId)
               .Include(comment => comment.User)
               .Include(comment => comment.Replies)
                   .ThenInclude(reply => reply.User)
               .Include(comment => comment.Replies)
               .ToListAsync();
        }

        // para recuperar un comentario incluyendo sus replies
        public async Task<Comment> GetByIdWithIncludeAsync(int commentId)
        {
           return await _dbContext.Set<Comment>()
                .Include(comment => comment.User)
                .Include(comment => comment.Replies)
                .SingleOrDefaultAsync(comment => comment.Id == commentId);
        }


    }
}
