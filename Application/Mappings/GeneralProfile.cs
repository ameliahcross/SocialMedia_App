using AutoMapper;
using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Application.ViewModels.Friendship;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Application.ViewModels.User;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region User
            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<User, SaveUserViewModel>()
                 .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                 .ForMember(dest => dest.File, opt => opt.Ignore())
                 .ReverseMap();

            CreateMap<User, EditUserViewModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.ActivationToken, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Friendships, opt => opt.Ignore());

            CreateMap<SaveUserViewModel, EditUserViewModel>()
                .ForMember(dest => dest.Password, act => act.Ignore())
                .ForMember(dest => dest.ConfirmPassword, act => act.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Password, act => act.Ignore())
                .ForMember(dest => dest.ConfirmPassword, act => act.Ignore());


            #endregion

            #region Post
            CreateMap<Post, PostViewModel>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
               .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
               .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            CreateMap<SavePostViewModel, Post>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Friendship
            CreateMap<Friendship, FriendshipViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ForMember(dest => dest.FriendId, opt => opt.MapFrom(src => src.FriendId))
                .ForMember(dest => dest.FriendName, opt => opt.MapFrom(src => src.Friend.Name))
                .ForMember(dest => dest.FriendLastName, opt => opt.MapFrom(src => src.Friend.LastName))
                .ForMember(dest => dest.FriendImageUrl, opt => opt.MapFrom(src => src.Friend.ImageUrl))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Friend, opt => opt.Ignore()); 

            CreateMap<Friendship, SaveFriendshipViewModel>()
                .ReverseMap();

            #endregion

            #region Comment
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl)) 
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore())

                .ForMember(dest => dest.ParentCommentId, opt => opt.MapFrom(src => src.ParentCommentId))
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<SaveCommentViewModel, Comment>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.Replies, opt => opt.Ignore())
                .ReverseMap();

            #endregion
        }
    }
}
