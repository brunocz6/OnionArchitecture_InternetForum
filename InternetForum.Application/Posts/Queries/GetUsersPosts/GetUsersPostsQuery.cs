using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Common.Mappings;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Queries.GetUsersPosts
{
    public class GetUsersPostsQuery : IRequest<IEnumerable<PostDto>>
    {
    }

    public class GetUsersPostsQueryHandler : IRequestHandler<GetUsersPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public GetUsersPostsQueryHandler(IPostRepository postRepository, IMapper mapper, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetUsersPostsQuery request, CancellationToken cancellationToken)
        {
            // Načtu příspěvky.
            var entities = _postRepository.GetPostsByUserId(_currentUserService.UserId);
            
            // Převedu na DTO objekty.
            var result = await entities
                .ProjectToListAsync<PostDto>(_mapper.ConfigurationProvider);

            foreach (var post in result)
            {
                post.AuthorName = await _identityService.GetUserNameAsync(post.AuthorId);

                foreach (var comment in post.Comments)
                {
                    comment.AuthorName = await _identityService.GetUserNameAsync(comment.AuthorId);
                }
            }
            
            return result;
        }
    }
}