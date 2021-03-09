using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Common.Mappings;
using InternetForum.Application.Common.Models;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Queries.GetUsersPostsWithPagination
{
    public class GetUsersPostsWithPaginationQuery : IRequest<PaginatedList<PostDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetUsersPostsQueryHandler : IRequestHandler<GetUsersPostsWithPaginationQuery, PaginatedList<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersPostsQueryHandler(IPostRepository postRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedList<PostDto>> Handle(GetUsersPostsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            // Načtu příspěvky.
            var result = await _postRepository.GetPostsByUserId(_currentUserService.UserId)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}