using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.ForumThreads.DTOs;
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

        public GetForumThreadQueryHandler(IForumThreadRepository forumThreadRepository, IMapper mapper)
        {
            _mapper = mapper;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<ForumThreadDto> Handle(GetForumThreadQuery request, CancellationToken cancellationToken)
        {
            var forumThread = await _forumThreadRepository.GetByIdAsync<ForumThreadDto>(request.Id);

            if (forumThread is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.Id);
            }
            
            return forumThread;
        }
    }
}