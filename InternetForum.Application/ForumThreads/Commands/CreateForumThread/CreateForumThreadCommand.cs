using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using InternetForum.Application.Common.Security;

namespace InternetForum.Application.ForumThreads.Commands.CreateForumThread
{
    [Authorize]
    public class CreateForumThreadCommand : IRequest<int>
    {
        /// <summary>
        /// Vrací nebo nastavuje název vlákna.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje popisek vlákna.
        /// </summary>
        public string Description { get; set; }
    }

    public class CreateForumThreadCommandHandler : IRequestHandler<CreateForumThreadCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IForumThreadRepository _forumThreadRepository;

        public CreateForumThreadCommandHandler(IUnitOfWork unitOfWork, IForumThreadRepository forumThreadRepository)
        {
            _unitOfWork = unitOfWork;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<int> Handle(CreateForumThreadCommand request, CancellationToken cancellationToken)
        {
            var entity = new ForumThread()
            {
                Name = request.Name,
                Description = request.Description
            };

            await _forumThreadRepository.AddOrUpdateAsync(entity);
            var result = await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
