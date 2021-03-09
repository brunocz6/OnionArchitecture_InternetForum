using InternetForum.Application.Common.Exceptions;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Security;

namespace InternetForum.Application.ForumThreads.Commands.DeleteForumThread
{
    [Authorize]
    public class DeleteForumThreadCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id vlákna, které se má smazat.
        /// </summary>
        public int Id { get; set; }
    }

    public class DeleteForumThreadCommandHandler : IRequestHandler<DeleteForumThreadCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForumThreadRepository _forumThreadRepository;

        public DeleteForumThreadCommandHandler(IUnitOfWork unitOfWork, IForumThreadRepository forumThreadRepository)
        {
            _unitOfWork = unitOfWork;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<int> Handle(DeleteForumThreadCommand request, CancellationToken cancellationToken)
        {
            var entity = await _forumThreadRepository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.Id);
            }

            _forumThreadRepository.Remove(entity);

            var result = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
