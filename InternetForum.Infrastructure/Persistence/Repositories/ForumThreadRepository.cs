using AutoMapper;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;

namespace InternetForum.Infrastructure.Persistence.Repositories
{
    public class ForumThreadRepository : Repository<ForumThread, int>, IForumThreadRepository
    {
        public ForumThreadRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
