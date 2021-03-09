using AutoMapper;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;

namespace InternetForum.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
