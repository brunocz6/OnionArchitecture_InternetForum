using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.ForumThreads.Queries
{
    public class GetForumThreadQuery : IRequest<ForumThreadDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id vlákna příspěvků.
        /// </summary>
        public int Id { get; set; }
    }
    
    public class GetForumThreadQueryHandler : IRequestHandler<GetForumThreadQuery, ForumThreadDto>
    {
        private readonly IMapper _mapper;
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IIdentityService _identityService;

        public GetForumThreadQueryHandler(IForumThreadRepository forumThreadRepository, IMapper mapper, IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<ForumThreadDto> Handle(GetForumThreadQuery request, CancellationToken cancellationToken)
        {
            var entity = await _forumThreadRepository.GetByIdAsync(request.Id);

            var forumThread = _mapper.Map<ForumThreadDto>(entity);

            if (forumThread is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.Id);
            }

            var posts = entity.Posts.ToList();
            forumThread.Posts = entity.Posts.ToList().Select(p => _mapper.Map<PostDto>(p));
            
            foreach (var post in forumThread.Posts)
            {
                post.AuthorName = await _identityService.GetUserNameAsync(post.AuthorId);
            }
            
            return forumThread;
        }
    }
}