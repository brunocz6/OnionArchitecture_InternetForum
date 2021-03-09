using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Exceptions;
using InternetForum.Application.Common.Security;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;

namespace InternetForum.Application.ForumThreads.Commands.EditForumThread
{
    [Authorize]
    public class EditForumThreadCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id vlákna příspěvků.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název vlákna.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje popisek vlákna.
        /// </summary>
        public string Description { get; set; }
    }

    public class EditForumThreadCommandHandler : IRequestHandler<EditForumThreadCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForumThreadRepository _forumThreadRepository;

        public EditForumThreadCommandHandler(IUnitOfWork unitOfWork, IForumThreadRepository forumThreadRepository)
        {
            _unitOfWork = unitOfWork;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<int> Handle(EditForumThreadCommand request, CancellationToken cancellationToken)
        {
            var entity = await _forumThreadRepository.GetByIdAsync(request.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(ForumThread), request.Id);
            }

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _forumThreadRepository.AddOrUpdateAsync(entity);
            var result = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}