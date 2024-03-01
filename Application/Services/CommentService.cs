using AutoMapper;
using SocialMedia_App.Core.Application.Interfaces.Repositories;
using SocialMedia_App.Core.Application.Interfaces.Services;
using SocialMedia_App.Core.Application.ViewModels.Comment;
using SocialMedia_App.Core.Application.ViewModels.Post;
using SocialMedia_App.Core.Domain.Entities;

namespace SocialMedia_App.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IMapper mapper)
            : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        // para listar los comentarios de cierto post
        public async Task<List<CommentViewModel>> GetAllByPostId(int postId)
        {
            var postComments = await _commentRepository.GetAllByPostIdWithIncludeAsync(postId);
            var commentsByPost = _mapper.Map<List<CommentViewModel>>(postComments);
            return commentsByPost;
        }

        // para devolver un comentario con sus replies
        public async Task<CommentViewModel> GetById(int commentId)
        {
            var comment = await _commentRepository.GetByIdWithIncludeAsync(commentId);
            var commentById = _mapper.Map<CommentViewModel>(comment); 
            return commentById;
        }







    }
}
