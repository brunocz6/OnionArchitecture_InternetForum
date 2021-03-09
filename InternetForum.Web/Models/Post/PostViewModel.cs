using System;
using InternetForum.Models;

namespace InternetForum.Web.Models.Post
{
	public class PostViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje ID příspěvku.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje nadpis příspěvku.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje obsah příspěvku.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje odkaz autora příspěvku.
		/// </summary>
		public LinkViewModel AuthorLink { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje datum přidání příspěvku.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje odkaz na vlákno příspěvku.
		/// </summary>
		public LinkViewModel ForumThreadLink { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje komentáře příspěvku.
		/// </summary>
		public CommentViewModel Comments { get; set; }

		/// <summary>
		/// Vytváří a vrací novou instanci <see cref="PostViewModel"/> modelu s vyplněnými daty z entity příspěvku.
		/// </summary>
		/// <param name="post">Entita příspěvku</param>
		/// <param name="commentsPageNumber">Číslo stránky komentářů</param>
		/// <param name="commentsPageSize">Počet komentářů na jedné stránce</param>
		public static PostViewModel FromDto(Post post, int commentsPageNumber, int commentsPageSize)
		{
			var model = new PostViewModel();
			model.Id = post.Id;
			model.Title = post.Title;
			model.Body = post.Body;
			model.AuthorLink = new LinkViewModel(post.AuthorId, post.Author.UserName);
			model.CreatedAt = post.CreatedAt;
			model.ForumThreadLink = new LinkViewModel(post.ForumThreadId, post.ForumThread.Name);

			// Naplním kolekci komentářů dle čísla stránky a max. počtu komentářů na stránku.
			model.Comments = PagingList.Create
			(
				post.Comments
				.ToList()
				.OrderByDescending(c => c.CreatedAt) // Seřadím od nejnovějšího po nejstarší.
				.Select(c =>
					CommentViewModel.CreateFromEntity(c)
				),
				commentsPageSize,
				commentsPageNumber
			);

			return model;
		}
	}
}