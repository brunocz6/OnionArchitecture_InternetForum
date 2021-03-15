using System.Collections.Generic;
using System.Linq;
using InternetForum.Application.Common.Models;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Web.Models.ForumThread;
using InternetForum.Web.Models.Post;

namespace InternetForum.Web.Models.Home
{
	public class HomeIndexViewModel
	{
		/// <summary>
		/// Vrací nebo nastavuje název vlákna příspěvků.
		/// </summary>
		public ForumThreadViewModel CurrentForumThread { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci základních informací o vláknech příspěvků.
		/// </summary>
		public IEnumerable<ForumThreadViewModel> ForumThreads { get; set; }

		/// <summary>
		/// Vrací nebo nastavuje kolekci příspěvků.
		/// </summary>
		public IEnumerable<PostViewModel> Posts { get; set; }

		public static HomeIndexViewModel Create(ForumThreadDto currentForumThread, IEnumerable<ForumThreadDto> forumThreads)
		{
			var model = new HomeIndexViewModel()
			{
				CurrentForumThread = ForumThreadViewModel.FromDto(currentForumThread),
				ForumThreads = forumThreads.Select(ForumThreadViewModel.FromDto),
				Posts = currentForumThread.Posts.Select(PostViewModel.FromDto)
			};

			return model;
		}
	}
}