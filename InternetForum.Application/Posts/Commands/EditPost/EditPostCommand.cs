using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Security;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Commands.EditPost
{
    [Authorize]
    public class EditPostCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id vlákna.
        /// </summary>
        public int Id { get; set; }
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

    public class EditPostCommandHandler : IRequestHandler<EditPostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public EditPostCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        public async Task<int> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            // Načtu příspěvek.
            var entity = await _postRepository.GetByIdAsync(request.Id);

            // Pokud nemám příspěvek, vyhodím výjimku.
            if (entity is null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }
            
            // Načtu nově zvolené vlákno příspěvku.
            var forumThread = await _postRepository.GetByIdAsync(request.ForumThreadId);

            // Pokud se mi nepodařilo nalézt vlákno, vyhodím výjimku.
            if (forumThread is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.ForumThreadId);
            }

            // Upravím data na entitě.
            entity.ForumThreadId = request.ForumThreadId;
            entity.Title = request.Title;
            entity.Body = request.Body;

            // Uložím změny.
            await _postRepository.AddOrUpdateAsync(entity, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}