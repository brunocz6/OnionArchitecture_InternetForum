using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Queries.GetPost
{
    public class GetPostQuery : IRequest<PostDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id příspěvku pro získání.
        /// </summary>
        public int Id { get; set; }
    }

    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IIdentityService _identityService;

        public GetPostQueryHandler(IPostRepository postRepository, IMapper mapper, IIdentityService identityService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            // Načtu příspěvěk.
            var post = await _postRepository.GetByIdAsync<PostDto>(request.Id);

            // Pokud se nepodařilo nalézt příspěvek se zvoleným Id, vyhodím výjimku.
            if (post is null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }
            
            post.AuthorName = await _identityService.GetUserNameAsync(post.AuthorId);
            
            foreach (var comment in post.Comments)
            {
                comment.AuthorName = await _identityService.GetUserNameAsync(comment.AuthorId);
            }
            
            return post;
        }
    }
}