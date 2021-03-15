using System.Threading.Tasks;
using InternetForum.Application.Comments.Commands.CreateComment;
using InternetForum.Application.Comments.Commands.DeleteComment;
using InternetForum.Application.Common.Models;
using InternetForum.Web.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetForum.Web.Controllers
{
    [Authorize]
	public class CommentController : BaseController
	{
		/// <summary>
		/// Akce pro přidání nového komentáře.
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> Add(CommentEditorViewModel model)
		{
			// Sestavím příkaz pro přidání komentáře pod příspěvek.
			var command = new CreateCommentCommand()
			{
				PostId = model.PostId,
				Body = model.Body
			};

			// Pošlu příkaz pro přidání komentáře pod příspěvek.
			await Mediator.Send(command);
			
			return new JsonResult(Result.Success());
		}

		/// <summary>
		/// Akce pro smazání komentáře s daným ID.
		/// </summary>
		/// <param name="commentId">Id komentáře určeného ke smazání</param>
		public async Task<IActionResult> Delete(int commentId)
		{
			// Sestavím příkaz pro odebrání komentáře.
			var command = new DeleteCommentCommand()
			{
				Id = commentId
			};

			// Pošlu příkaz pro smazání komentáře.
			await Mediator.Send(command);
			
			return new JsonResult(Result.Success());
		}
	}
}