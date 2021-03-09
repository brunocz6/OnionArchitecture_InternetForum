using System.ComponentModel.DataAnnotations;
using InternetForum.Application.Posts.DTOs;

namespace InternetForum.Web.Models.Post
{
	/// <summary>
	/// ViewModel pro editaci příspěvku.
	/// </summary>
	public class PostEditorViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje Id příspěvku.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje nadpis příspěvku.
		/// </summary>
		[Display(Name = "Nadpis")]
		public string Title { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje obsah příspěvku.
		/// </summary>
		[Display(Name = "Obsah")]
		public string Body { get; set; }

		/// <summary>
		/// Vrací ViewModel typu <see cref="PostEditorViewModel"/> vytvořený z DB entity.
		/// </summary>
		/// <param name="post">Datový objekt příspěvku.</param>
		public static PostEditorViewModel FromDto(PostDto post)
		{
			var model = new PostEditorViewModel()
			{
				Id = post.Id,
				Title = post.Title,
				Body = post.Body
			};

			return model;
		}

		/// <summary>
		/// Vrací tento model převedený na datový objekt.
		/// </summary>
		public PostDto ToDto()
		{
			var dto = new PostDto()
			{
				Id = this.Id,
				Title = this.Title,
				Body = this.Body
			};

			return dto;
		}
	}
}