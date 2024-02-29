using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Repositories;

namespace SocialMedia_App.Core.Application.Services
{
    public class FriendshipService : GenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public FriendshipService(IFriendshipRepository friendshipRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IMapper mapper) : base(friendshipRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _friendshipRepository = friendshipRepository;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task<bool> AreFriendsAsync(int userId, int friendId)
        {
            var friends = await _friendshipRepository.GetAllFriendsByUserIdAsync(userId);
            return friends.Any(friends => friends.FriendId == friendId);
        }

        public async Task<IEnumerable<UserViewModel>> GetFriendsByUserId(int userId)
        {

        }

        public async Task<IEnumerable<PostViewModel>> GetFriendPostsByUserId(int userId)
        {

        }


    }
}
