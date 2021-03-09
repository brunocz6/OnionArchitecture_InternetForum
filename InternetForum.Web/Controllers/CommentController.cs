using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetForum.Web.Controllers
{
    [Authorize]
	public class CommentController : BaseController
	{
		public CommentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : base(unitOfWork, userManager)
		{
		}

		/// <summary>
		/// Akce pro přidání nového komentáře.
		/// </summary>
		public IActionResult Add( model)
		{
			// Zkontroluju, jestli model prošel validací.
			if (ModelState.IsValid)
			{
				// Vytvořím entitu komentáře.
				var comment = model.CreateEntity(GetCurrentUserId());

				// Přidám entitu do databáze.
				this.unitOfWork.CommentRepository.Add(comment);

				// Uložím změny v databázi.
				this.unitOfWork.Save();
			}

			// Vrátím "redirect" na detail příspěvku, ke kterému byl tento komentář přidán.
			return RedirectToAction("Detail", "Post", new { id = model.PostId });
		}

		/// <summary>
		/// Akce pro smazání komentáře s daným ID.
		/// </summary>
		/// <param name="commentId">Id komentáře určeného ke smazání</param>
		public IActionResult Delete(int commentId)
		{
			// Načtu entitu komentáře z databáze.
			var comment = this.unitOfWork.CommentRepository.GetById(commentId);

			// Zkontroluju, jestli komentář s daným Id existuje a jestli je uživatel oprávněn komentář odebrat.
			if (comment != null && comment.AuthorId == GetCurrentUserId())
			{
				// Smažu komentář z databáze.
				this.unitOfWork.CommentRepository.Remove(comment);
				// Uložím změny v databázi
				this.unitOfWork.Save();

				// Vrátím zprávu informující o úspěchu.
				return Json(new { success = true, responseText = "Komentář byl úspěšně odebrán." });
			}
			else
			{
				// Vrátím zprávu informující o neúspěchu.
				return Json(new { success = false, responseText = "Komentář se nepodařilo odebrat." });
			}
		}
	}
}