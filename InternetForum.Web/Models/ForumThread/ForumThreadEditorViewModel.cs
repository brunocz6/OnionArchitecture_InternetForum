using System.ComponentModel.DataAnnotations;
using InternetForum.Application.ForumThreads.DTOs;

namespace InternetForum.Web.Models.ForumThread
{
	public class ForumThreadEditorViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje Id příspěvku.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje název vlákna příspěvků.
		/// </summary>
		[Display(Name = "Název")]
		public string Name { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje popis vlákna.
		/// </summary>
		[Display(Name = "Popis")]
		public string Description { get; set; }

		/// <summary>
		/// Vytváří a vrací novou instanci <see cref="ForumThreadEditorViewModel"/> modelu s vyplněnými daty z DTO objektu.
		/// </summary>
		/// <param name="forumThread">Datový objekt vlákna příspěvků</param>
		public static ForumThreadEditorViewModel FromDto(ForumThreadDto forumThread)
		{
			var model = new ForumThreadEditorViewModel()
			{
				Id = forumThread.Id,
				Name = forumThread.Name,
				Description = forumThread.Description
			};

			return model;
		}

		/// <summary>
		/// Aktualizuje hodnoty v entitě vlákna příspěvků informacemi z tohoto modelu.
		/// </summary>
		public ForumThreadDto ToDto()
		{
			var dto = new ForumThreadDto()
			{
				Id = this.Id,
				Name = this.Name,
				Description = this.Description
			};

			return dto;
		}
	}
}