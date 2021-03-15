using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Application.ForumThreads.Queries;
using InternetForum.Application.Posts.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

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
		/// Vrací nebo nastavuje Id vlákna, pod které patří tento příspěvěk.
		/// </summary>
		[Display(Name = "Vlákno příspěvku")]
		public int ForumThreadId { get; set; }
		
		/// <summary>
		/// Vrací nebo nastavuje seznam dostupných vláken.
		/// </summary>
		public IEnumerable<SelectListItem> ForumThreads { get; set; }

		/// <summary>
		/// Vrací ViewModel typu <see cref="PostEditorViewModel"/> vytvořený z DB entity.
		/// </summary>
		/// <param name="post">Datový objekt příspěvku.</param>
		public static PostEditorViewModel FromDto(PostDto post)
		{
			var model = new PostEditorViewModel
			{
				Id = post.Id,
				ForumThreadId = post.ForumThreadId,
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

		public async Task Fetch(ISender mediator)
		{
			// Sestavím query pro všech dostupných vláken příspěvků.
			var query = new GetForumThreadsQuery();

			// Pošlu dotaz na získání příspěvku.
			var forumThreads = await mediator.Send(query);

			this.ForumThreads = forumThreads.Select(ft =>
				new SelectListItem(ft.Name, ft.Id.ToString())
			);
		}
	}
}