using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Interfaces;
using InternetForum.Application.Common.Security;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.Comments.Commands.DeleteComment
{
    [Authorize]
    public class DeleteCommentCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komentáře, který se má smazat.
        /// </summary>
        public int Id { get; set; }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, ICommentRepository commentRepository, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            // Načtu komentář.
            var entity = await _commentRepository.GetByIdAsync(request.Id);

            // Pokud se komentář nepodařilo nalézt, vyhodím výjimku.
            if (entity is null)
            {
                throw new NotFoundException(nameof(Comment), request.Id);
            }

            // Pokud se uživatel pokouší smazat příspěvek, který mu nepatří, vyhodím výjimku.
            if (entity.AuthorId != _currentUserService.UserId)
            {
                throw new ForbiddenAccessException();
            }

            // Uložím změny.
            _commentRepository.Remove(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}