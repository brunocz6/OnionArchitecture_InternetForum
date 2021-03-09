using System;
using InternetForum.Application.Comments.DTOs;

namespace InternetForum.Web.Models
{
	public class CommentViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje ID komentáře.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje obsah komentáře.
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje jméno autora tohoto komentáře.
		/// </summary>
		public string AuthorUserName { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje, kdy byl tento komentář vytvořen.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Vytváří a vrací novou instanci <see cref="CommentViewModel"/> modelu s vyplněnými daty z entity.
		/// </summary>
		/// <param name="comment">Entita komentáře</param>
		public static CommentViewModel FromDto(CommentDto comment)
		{
			var model = new CommentViewModel
			{
				Id = comment.Id,
				Body = comment.Body,
				AuthorUserName = comment.Author.UserName,
				CreatedAt = comment.CreatedAt
			};

			return model;
		}
	}
}