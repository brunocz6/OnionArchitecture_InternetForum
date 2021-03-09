using System.Threading;
using System.Threading.Tasks;

namespace InternetForum.Domain.Repositories
{
    /// <summary>
    /// Jednotka pro manipulaci s datovým zdrojem (např. ORM databází).
    /// </summary>
    public interface IUnitOfWork
    {
		int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
