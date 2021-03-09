using System.Linq;
using InternetForum.Domain.Entities;

namespace InternetForum.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post, int>
    {
        IQueryable<Post> GetPostsByUserId(string userId);

        IQueryable<Post> GetPostsByForumThreadId(int forumThreadId);
    }
}
