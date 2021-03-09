using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Security;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Commands.CreatePost
{
    [Authorize]
    public class CreatePostCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje, kterého vlákna je příspěvek součástí.
        /// </summary>
        public int ForumThreadId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje nadpis příspěvku.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje obsah příspěvku.
        /// </summary>
        public string Body { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var forumThread = await _postRepository.GetByIdAsync(request.ForumThreadId);

            if (forumThread is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.ForumThreadId);
            }
            
            var entity = new Post()
            {
                ForumThreadId = request.ForumThreadId,
                Title = request.Title,
                Body = request.Body
            };

            await _postRepository.AddOrUpdateAsync(entity, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}