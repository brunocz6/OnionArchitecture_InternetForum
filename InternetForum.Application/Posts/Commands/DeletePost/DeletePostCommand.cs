using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id příspěvku, který se má smazat.
        /// </summary>
        public int Id { get; set; }
    }
    
    public class DeletePostThreadCommandHandler : IRequestHandler<DeletePostCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public DeletePostThreadCommandHandler(IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
        }

        public async Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            // Načtu příspěvek.
            var entity = await _postRepository.GetByIdAsync(request.Id);

            // Pokud se příspěvek nepodařilo nalézt, vyhodím výjimku.
            if (entity is null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            // Uložím změny.
            _postRepository.Remove(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}