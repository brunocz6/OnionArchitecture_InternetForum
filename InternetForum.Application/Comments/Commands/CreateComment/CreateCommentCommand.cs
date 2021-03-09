using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Common.Security;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Comments.Commands.CreateComment
{
    [Authorize]
    public class CreateCommentCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id příspěvku, pod který se má komentář přidat.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje text komentáře.
        /// </summary>
        public string Body { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IPostRepository postRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);

            if (post is null)
            {
                throw new NotFoundException(nameof(Post), request.PostId);
            }
            
            var entity = new Comment()
            {
                AuthorId = _currentUserService.UserId,
                PostId = request.PostId,
                Body = request.Body
            };

            await _commentRepository.AddOrUpdateAsync(entity, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}