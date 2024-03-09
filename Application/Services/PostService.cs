using AutoMapper;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Repositories;

namespace SocialMedia_App.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Post>, IPostService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IFriendshipRepository friendshipRepository,
           IMapper mapper) : base (postRepository, mapper)
        {
            _friendshipRepository = friendshipRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // para crear la lista de publicaciones del usuario loggueado
        public async Task<List<PostViewModel>> GetAllPostByUserIdWithIncludeAsync(int userId)
        {
            var userPosts = await _postRepository.GetAllPostByUserIdWithIncludeAsync(userId);
            var postByTheUser = _mapper.Map<List<PostViewModel>>(userPosts);
            return postByTheUser;
        }

        // para crear la lista de publicaciones de los amigos de cierto usuario
        public async Task<List<PostViewModel>> GetFriendPostsByUserId(int userId)
        {
            var friendships = await _friendshipRepository.GetAllFriendsByUserIdAsync(userId);
            var friendIds = friendships.Select(friend => friend.FriendId).ToList();
            var friendsPosts = await _postRepository.GetPostsByUsersIdsWithIncludeAsync(friendIds);

            var postByFriendsViewModels = _mapper.Map<List<PostViewModel>>(friendsPosts);
            return postByFriendsViewModels;
        }

        // para publicar una imagen
        public async Task PostImage(int postId, string imageUrl)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post != null)
            {
                post.ImageUrl = imageUrl;
                await _postRepository.PostImageAsync(post);
            }
        }
    }

    
}
