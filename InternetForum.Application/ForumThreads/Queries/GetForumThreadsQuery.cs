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

namespace InternetForum.Application.ForumThreads.Queries
{
    public class GetForumThreadsQuery : IRequest<IEnumerable<ForumThreadDto>>
    {
    }

    public class GetForumThreadsQueryHandler : IRequestHandler<GetForumThreadsQuery, IEnumerable<ForumThreadDto>>
    {
        private readonly IMapper _mapper;
        private readonly IForumThreadRepository _forumThreadRepository;

        public GetForumThreadsQueryHandler(IForumThreadRepository forumThreadRepository, IMapper mapper)
        {
            _mapper = mapper;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<IEnumerable<ForumThreadDto>> Handle(GetForumThreadsQuery request, CancellationToken cancellationToken)
        {
            var forumThreads = _forumThreadRepository
                .GetAll()
                .ProjectTo<ForumThreadDto>(_mapper.ConfigurationProvider)
                .ToList();

            return forumThreads;
        }
    }
}
