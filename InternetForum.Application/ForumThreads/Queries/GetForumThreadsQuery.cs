using AutoMapper;
using AutoMapper.QueryableExtensions;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Common.Mappings;

namespace InternetForum.Application.ForumThreads.Queries
{
    public class GetForumThreadsQuery : IRequest<IEnumerable<ForumThreadDto>>
    {
    }

    public class GetForumThreadsQueryHandler : IRequestHandler<GetForumThreadsQuery, IEnumerable<ForumThreadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IIdentityService _identityService;

        public GetForumThreadsQueryHandler(IForumThreadRepository forumThreadRepository, IMapper mapper, IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<IEnumerable<ForumThreadDto>> Handle(GetForumThreadsQuery request, CancellationToken cancellationToken)
        {
            var entities = _forumThreadRepository.GetAll();

            var result = await entities
                .ProjectToListAsync<ForumThreadDto>(_mapper.ConfigurationProvider);

            foreach (var forumThread in result)
            {
                foreach (var post in forumThread.Posts)
                {
                    post.AuthorName = await _identityService.GetUserNameAsync(post.AuthorId);
                }
            }
            
            return result;
        }
    }
}
