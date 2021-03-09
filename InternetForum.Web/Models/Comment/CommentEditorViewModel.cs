using System;
using System.ComponentModel.DataAnnotations;
using InternetForum.Application.Comments.DTOs;
using InternetForum.Application.Common.Mappings;
using InternetForum.Application.Posts.DTOs;
using InternetForum.Domain.Entities;

namespace InternetForum.Web.Models.Comment
{
	public class CommentEditorViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje obsah komentáře. 
		/// </summary>
		public string Body { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje ke kterému příspěvku komentář patří.
		/// </summary>
		public int PostId { get; set; }

		/// <summary>
		/// Vrací <see cref="Comment"/> entitu naplněnou daty z tohoto modelu.
		/// </summary>
		/// <param name="authorId">Id autora komentáře</param>
		public CommentDto ToDto()
		{
			var dto = new CommentDto()
			{
				Body = this.Body,
				PostId = this.PostId
			};

			return dto;
		}
	}
}