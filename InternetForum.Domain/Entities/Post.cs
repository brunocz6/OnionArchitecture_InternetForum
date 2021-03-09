using InternetForum.Domain.Common;
using System.Collections.Generic;

namespace InternetForum.Domain.Entities
{
    /// <summary>
    /// Entita příspěvku.
    /// </summary>
    public class Post : AuditableEntity<int>
	{
		/// <summary>
		/// Vrací nebo nastavuje nadpis příspěvku.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje obsah příspěvku.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id vlákna, kterého je příspěvek součástí.
		/// </summary>
		public int ForumThreadId { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje, kterého vlákna je příspěvek součástí.
		/// </summary>
		public virtual ForumThread ForumThread { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id autora.
		/// </summary>
		public string AuthorId { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci komentářů tohoto příspěvku.
		/// </summary>
		public virtual ICollection<Comment> Comments { get; set; }
	}
}