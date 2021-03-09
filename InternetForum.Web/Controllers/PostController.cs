using System.Threading.Tasks;
using InternetForum.Application.Posts.Queries.GetUsersPostsWithPagination;
using InternetForum.Web.Models.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetForum.Web.Controllers
{
	public class PostController : BaseController
	{
		/// <summary>
		/// Akce pro zobrazení příspěvků přihlášeného uživatele.
		/// </summary>
		/// <param name="page">číslo stránky</param>
		public async Task<IActionResult> MyPosts(int page = 1)
		{
			var query = new GetUsersPostsWithPaginationQuery()
			{
				PageNumber = page
			};

			var result = await Mediator.Send(query);
			
			var posts = this.unitOfWork.PostRepository
				.GetUsersPosts(GetCurrentUserId())
				.ToList();

			var model = posts.Select(p => PostDetailsViewModel.CreateFromEntity(p));

			return View(model);
		}

		/// <summary>
		/// Akce pro vytvoření nového příspěvku (vrací formulář).
		/// </summary>
		[Authorize]
		public async Task<IActionResult> Create()
		{
			// Načtu z databáze všechna dostupná vlákna příspěvků.
			var forumThreads = this.unitOfWork.ForumThreadRepository.GetAll().ToList();

			// Vytvořím model.
			var model = new CreatePostViewModel(forumThreads);

			return View(model);
		}

		/// <summary>
		/// Akce pro vytvoření nového příspěvku (zpracování dat z formuláře).
		/// </summary>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(CreatePostViewModel model)
		{
			// Zkontroluji, jestli model prošel validací.
			if (ModelState.IsValid)
			{
				// Vytvořím entitu příspěvku a naplním ji daty z modelu.
				var post = model.CreateEntity(GetCurrentUser().Id);

				// Přidám entitu do databáze.
				this.unitOfWork.PostRepository.Add(post);

				// Uložím změny v databázi.
				this.unitOfWork.Save();

				// Přesměruji uživatele na hlavní stránku.
				return RedirectToAction("Index", "Home");
			}

			// Pokud model neprojde validací, objeví se uživateli formulář s chybovými hláškami.

			// Vložím do modelu seznam dostupných vláken příspěvků.
			model.ForumThreads = this.unitOfWork.ForumThreadRepository
				.GetAll()
				.ToList()
				.Select(ft => new SelectListItem()
				{
					Text = ft.Name,
					Value = ft.Id.ToString()
				});

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
			// Načtu příspěvek z databáze.
			var post = this.unitOfWork.PostRepository.GetById(id);

			// Převedu entitu na model.
			var model = EditPostViewModel.CreateFromEntity(post);

			return View(model);
		}

		/// <summary>
		/// Akce pro upravení příspěvku (zpracování dat z formuláře).
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Edit(EditPostViewModel model)
		{
			// Zkontroluji, jestli model prošel validací.
			if (ModelState.IsValid)
			{
				// Načtu entitu příspěvek s daným Id.
				var post = this.unitOfWork.PostRepository.GetById(model.Id);

				// Pokud je aktuálně přihlášený uživatel autorem příspěvku, uložím změny.
				if (post.AuthorId == GetCurrentUserId())
				{
					model.UpdateEntity(post);
					this.unitOfWork.PostRepository.Update(post);
				}

				// Přesměruji uživatele na hlavní stránku.
				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		/// <summary>
		/// Akce pro zobrazení detailu příspěvku.
		/// </summary>
		/// <param name="id">Id příspěvku</param>
		/// <param name="page">číslo stránky komentářů</param>
		public async Task<IActionResult> Detail(int id, int page = 1)
		{
			// Načtu entitu příspěvku.
			var post = this.unitOfWork.PostRepository.GetById(id);

			// Pokud příspěvek s daným Id nebyl v databázi nalezen, vrátím ERROR 404.
			if (post is null)
				return NotFound();

			// Převedu entitu na model.
			var model = PostViewModel.CreateFromEntity(post, page, 10);

			model.Comments.Action = nameof(Detail);

			return View(model);
		}

		/// <summary>
		/// Akce pro smazání příspěvku.
		/// </summary>
		/// <param name="id">Id příspěvku určeného ke smazání</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Delete(int id)
		{
			// Načtu entitu příspěvku.
			var post = this.unitOfWork.PostRepository.GetById(id);

			// Pokud je aktuálně přihlášený uživatel autorem příspěvku a pokud byl záznam
			// v databázi nalezen, smažu ho.
			if (post != null && post.Author.Id == GetCurrentUserId())
			{
				// Odstraním příspěvek z databáže.
				this.unitOfWork.PostRepository.Remove(post);

				// Uložím změny v databázi.
				this.unitOfWork.Save();
			}

			// Přesměruji uživatele na seznam jeho příspěvků.
			return RedirectToAction(nameof(MyPosts));
		}
	}
}