using System.Linq;
using System.Threading.Tasks;
using InternetForum.Application.Posts.Commands.CreatePost;
using InternetForum.Application.Posts.Commands.DeletePost;
using InternetForum.Application.Posts.Commands.EditPost;
using InternetForum.Application.Posts.Queries.GetPost;
using InternetForum.Application.Posts.Queries.GetUsersPosts;
using InternetForum.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetForum.Web.Controllers
{
	public class PostController : BaseController
	{
		/// <summary>
		/// Akce pro zobrazení příspěvků přihlášeného uživatele.
		/// </summary>
		/// <param name="page">číslo stránky</param>
		[Authorize]
		public async Task<IActionResult> MyPosts()
		{
			var query = new GetUsersPostsQuery();

			var result = await Mediator.Send(query);

			var model = result.Select(PostViewModel.FromDto);

			return View(model);
		}

		/// <summary>
		/// Akce pro vytvoření nového příspěvku (vrací formulář).
		/// </summary>
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var model = new PostEditorViewModel();
			
			await model.Fetch(Mediator);

			return View(model);
		}

		/// <summary>
		/// Akce pro vytvoření nového příspěvku (zpracování dat z formuláře).
		/// </summary>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(PostEditorViewModel model)
		{
			var command = new CreatePostCommand()
			{
				ForumThreadId = model.ForumThreadId,
				Title = model.Title,
				Body = model.Body
			};

			var result = await Mediator.Send(command);
			
			if (result > 0)
			{
				return RedirectToAction("Index", "Home");
			}

			await model.Fetch(Mediator);

			return View(model);
		}

		/// <summary>
		/// Akce pro úpravení příspěvku (vrací formulář).
		/// </summary>
		/// <param name="id">Id příspěvku</param>
		/// <returns></returns>
		[Authorize]
		public async Task<IActionResult> Edit(int id)
		{
			// Sestavím query pro získání příspěvku.
			var queryGetPost = new GetPostQuery()
			{
				Id = id
			};

			// Pošlu dotaz na získání příspěvku.
			var post = await Mediator.Send(queryGetPost);
			
			// Převedu entitu na model.
			var model = PostEditorViewModel.FromDto(post);

			await model.Fetch(Mediator);

			return View(model);
		}

		/// <summary>
		/// Akce pro upravení příspěvku (zpracování dat z formuláře).
		/// </summary>
		/// <param name="model"></param>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Edit(PostEditorViewModel model)
		{
			var command = new EditPostCommand()
			{
				Id = model.Id,
				ForumThreadId = model.ForumThreadId,
				Title = model.Title,
				Body = model.Body
			};

			var result = await Mediator.Send(command);

			if (result > 0)
			{
				// Přesměruji uživatele na hlavní stránku.
				return RedirectToAction("Index", "Home");
			}

			await model.Fetch(Mediator);
			
			return View(model);
		}

		/// <summary>
		/// Akce pro zobrazení detailu příspěvku.
		/// </summary>
		/// <param name="id">Id příspěvku</param>
		public async Task<IActionResult> Detail(int id)
		{
			var query = new GetPostQuery()
			{
				Id = id
			};

			var post = await Mediator.Send(query);

			var model = PostViewModel.FromDto(post);

			return View(model);
		}

		/// <summary>
		/// Akce pro smazání příspěvku.
		/// </summary>
		/// <param name="id">Id příspěvku určeného ke smazání</param>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			// Sestavím příkaz pro smazání příspěvku.
			var command = new DeletePostCommand()
			{
				Id = id
			};

			// Pošlu příkaz pro smazání příspěvku.
			var result = await Mediator.Send(command);
			
			// Přesměruji uživatele na seznam jeho příspěvků.
			return RedirectToAction(nameof(MyPosts));
		}
	}
}