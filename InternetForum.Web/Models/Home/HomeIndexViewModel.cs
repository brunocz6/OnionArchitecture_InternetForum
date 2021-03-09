using System.Collections.Generic;
using InternetForum.Web.Models.ForumThread;

namespace InternetForum.Models
{
	public class HomeIndexViewModel
	{
		/// <summary>
		/// Vrací prázdnou instanci <see cref="HomeIndexViewModel"/> modelu.
		/// </summary>
		public HomeIndexViewModel()
		{

		}

		/// <summary>
		/// Vrací instanci <see cref="HomeIndexViewModel"/> modelu s předvyplněnými hodnotami.
		/// </summary>
		/// <param name="posts">Kolekce příspěvků</param>
		/// <param name="forumThreads">Kolekce dostupných vláken příspěvků</param>
		/// <param name="forumThread">Objekt s informacemi o aktuálně zvoleném vláknu příspěvků</param>
		public HomeIndexViewModel(PagingList<PostPreviewViewModel> posts, IEnumerable<ForumThreadViewModel> forumThreads)
		{
			this.Posts = posts;
			this.ForumThreads = forumThreads;
		}

		/// <summary>
		/// Vrací nebo nastavuje název vlákna příspěvků.
		/// </summary>
		public ForumThreadInfoPanelViewModel CurrentForumThread { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci základních informací o vláknech příspěvků.
		/// </summary>
		public IEnumerable<ForumThreadViewModel> ForumThreads { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci příspěvků.
		/// </summary>
		public PagingList<PostPreviewViewModel> Posts { get; set; }
	}
}