﻿using AutoMapper;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Domain.Entities;
using SocialMedia_App.Infrastructure.Persistence.Repositories;

namespace SocialMedia_App.Core.Application.Services
{
    public class FriendshipService : GenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IMapper _mapper;

        public FriendshipService(IFriendshipRepository friendshipRepository,IMapper mapper)
            : base(friendshipRepository, mapper)
        {
            _friendshipRepository = friendshipRepository;
            _mapper = mapper;
        }

        // para crear una nueva amistad
        public async Task AddFriendshipAsync(int userId, int friendId)
        {
            var newFriendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            // para hacer la amistad bidireccional
            var newFriendship2 = new Friendship 
            {
                UserId = friendId,
                FriendId = userId
            };

            await _friendshipRepository.AddAsync(newFriendship);
            await _friendshipRepository.AddAsync(newFriendship2);
        }

        // para obtener la lista de los amigos de cierto usuario
        public async Task<List<FriendListViewModel>> GetFriendsByUserId(int userId)
        {
            var friendships = await _friendshipRepository.GetAllFriendsByUserIdAsync(userId);
            var friendListViewModel = _mapper.Map<List<FriendListViewModel>>(friendships);
            return friendListViewModel;
        }

        // para saber si dos usuarios son amigos
        public async Task<bool> AreFriendsAsync(int userId, int friendId)
        {
            var friends = await _friendshipRepository.GetAllFriendsByUserIdAsync(userId);
            return friends.Any(friends => friends.FriendId == friendId);
        }



    }
}
