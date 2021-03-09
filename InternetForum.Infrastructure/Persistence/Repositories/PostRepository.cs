using System.Linq;
using AutoMapper;
using InternetForum.Domain.Entities;
using InternetForum.Domain.Repositories;

namespace InternetForum.Infrastructure.Persistence.Repositories
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        /// <summary>
		/// Vrací příspěvky v databázi patřící pod dané vlákno.
		/// </summary>
		/// <param name="forumThreadId">Id vlákna</param>
		/// <returns>Nejnovější příspěvky jako <see cref="IEnumerable{Post}"/></returns>
		public IQueryable<Post> GetPostsByForumThreadId(int forumThreadId)
        {
	        var result = Find(p => p.ForumThreadId == forumThreadId)
		        .OrderByDescending(p => p.CreatedAt);

	        return result;
        }

		/// <summary>
		/// Vrací všechny příspěvky daného uživatele.
		/// </summary>
		/// <param name="userId">Id uživatele (GUID)</param>
		public IQueryable<Post> GetPostsByUserId(string userId)
		{
			var result = Find(p => p.AuthorId == userId);
			return result;
		}
    }
}
