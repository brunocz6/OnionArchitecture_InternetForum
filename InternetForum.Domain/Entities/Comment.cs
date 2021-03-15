using InternetForum.Domain.Common;

namespace InternetForum.Domain.Entities
{
    /// <summary>
    /// Entita komentáře.
    /// </summary>
    public class Comment : AuditableEntity<int>
	{
		public Comment()
		{
			
		}
		
		/// <summary>
		/// Vrací nebo nastavuje text komentáře.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id autora.
		/// </summary>
		public string AuthorId { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje Id příspěvku, pod který komentář patří.
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje příspěvek, pod který komentář patří.
		/// </summary>
		public virtual Post Post { get; set; }
	}
}