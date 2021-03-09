using InternetForum.Domain.Common;
using System.Collections.Generic;

namespace InternetForum.Domain.Entities
{
    /// <summary>
    /// Entita vlákna příspěvků.
    /// </summary>
    public class ForumThread : AuditableEntity<int>
	{
		/// <summary>
		/// Vrací nebo nastavuje název vlákna.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje popis vlákna.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje odběratele tohoto vlákna.
		/// </summary>
		public virtual ICollection<ForumThreadUser> Subscribers { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje příspěvky, které jsou součástí tohoto vlákna.
		/// </summary>
		public virtual ICollection<Post> Posts { get; set; }
	}
}