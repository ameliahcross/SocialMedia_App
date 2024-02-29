using Microsoft.EntityFrameworkCore;
using SocialMedia_App.Core.Application.Helpers;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Contexts;

namespace SocialMedia_App.Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        private readonly ApplicationContext _dbContext;
        public FriendshipRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Friendship>> GetAllFriendsByUserIdAsync(int userId)
        {
            return await _dbContext.Set<Friendship>()
               .Where(friendship => friendship.UserId == userId)
               .Include(friendship => friendship.Friend)
               .ToListAsync();
        }

        public async Task DeleteFriendshipAsync(int userId, int friendId)
        {
            var friendship = await _dbContext.Set<Friendship>()
                .FirstOrDefaultAsync(friendship => friendship.UserId == userId && friendship.FriendId == friendId);

            if (friendship != null)
            {
                _dbContext.Set<Friendship>().Remove(friendship);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
