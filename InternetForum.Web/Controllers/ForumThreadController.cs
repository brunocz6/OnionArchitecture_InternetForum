using System.Linq;
using System.Threading.Tasks;
using InternetForum.Application.ForumThreads.Commands.CreateForumThread;
using InternetForum.Application.ForumThreads.Commands.DeleteForumThread;
using InternetForum.Application.ForumThreads.Commands.EditForumThread;
using InternetForum.Application.ForumThreads.Queries;
using InternetForum.Web.Models.ForumThread;
using Microsoft.AspNetCore.Mvc;

namespace InternetForum.Web.Controllers
{
	/// <summary>
	/// Řadič s akcemi souvisejíci s vlákny příspěvků (k těmto akcím má přístup pouze uživatel s rolí 'Administrator').
	/// </summary>
	public class ForumThreadController : BaseController
	{
		/// <summary>
		/// Akce pro přidání nového vlákna příspěvků (vrací formulář).
		/// </summary>
		/// <returns></returns>
		public IActionResult Create()
		{
			// Vytvořím model pro přidání nového vlákna příspěvků.
			var model = new ForumThreadEditorViewModel();

			return View(model);
		}

		/// <summary>
		/// Akce pro přidání nového vlákna příspěvků (zpracování dat z formuláře).
		/// </summary>
		/// <param name="model">model nového vlákna příspěvků</param>
		[HttpPost]
		public async Task<IActionResult> Create(ForumThreadEditorViewModel model)
		{
			var command = new CreateForumThreadCommand()
			{
				Name = model.Name,
				Description = model.Description
			};
			
			var result = await Mediator.Send(command);

			if (result > 0)
			{
				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		/// <summary>
		/// Akce vracející seznam všech vláken příspěvků.
		/// </summary>
		public async Task<IActionResult> List()
		{
			var query = new GetForumThreadsQuery();

			var result = await Mediator.Send(query);

			// Převedu datové objekty na modely.
			var models = result.Select(ForumThreadViewModel.FromDto);

			return View(models);
		}

		/// <summary>
		/// Akce pro editaci vlákna příspěvků (vrací formulář).
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> Edit(int id)
		{
			var query = new GetForumThreadQuery()
			{
				Id = id
			};

			var result = await Mediator.Send(query);

			var model = ForumThreadEditorViewModel.FromDto(result);

			return View(model);
		}

		/// <summary>
		/// Akce pro editaci vlákna příspěvků (zpracování dat z formuláře).
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(ForumThreadEditorViewModel model)
		{
			var command = new EditForumThreadCommand()
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description
			};

			var result = await Mediator.Send(command);

			if (result > 0)
			{
				return RedirectToAction("List", "ForumThread");
			}

			return View(model);
		}

		/// <summary>
		/// Akce pro smazání vlákna příspěvků.
		/// </summary>
		/// <param name="id">Id vlákna příspěvků určeného ke smazání.</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteForumThreadCommand()
			{
				Id = id
			};

			await Mediator.Send(command);

			return RedirectToAction("List", "ForumThread");
		}
	}
}