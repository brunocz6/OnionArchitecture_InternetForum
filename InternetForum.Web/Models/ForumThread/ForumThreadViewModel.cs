using InternetForum.Application.ForumThreads.DTOs;

namespace InternetForum.Web.Models.ForumThread
{
	public class ForumThreadViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje Id tohoto vlákna.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje název vlákna.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje popis vlákna.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Vytváří a vrací novou instanci <see cref="ForumThreadViewModel"/> modelu
		/// s vyplněnými daty z datového objektu vlákna příspěvků.
		/// </summary>
		/// <param name="forumThread">Datový objekt vlákna příspěvků</param>
		public static ForumThreadViewModel FromDto(ForumThreadDto forumThread)
		{
			var model = new ForumThreadViewModel()
			{
				Id = forumThread.Id,
				Name = forumThread.Name,
				Description = forumThread.Description
			};

			return model;
		}
	}
}