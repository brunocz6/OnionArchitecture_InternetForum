using System.Linq;
using System.Threading.Tasks;
using InternetForum.Application.ForumThreads.DTOs;
using InternetForum.Application.ForumThreads.Queries;
using InternetForum.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace InternetForum.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<IActionResult> Index(int forumThreadId)
        {
            // Vytvořím query pro získání vlákna příspěvků se zvoleným Id.
            var queryGetForumThread = new GetForumThreadQuery()
            {
                Id = forumThreadId
            };
            
            // Vytvořím query pro získání všech vláken příspěvků.
            var queryGetForumThreads = new GetForumThreadsQuery();
            
            // Pošlu dotaz pro získání všech vláken příspěvků.
            var forumThreads = (await Mediator.Send(queryGetForumThreads)).ToList();

            // Pošlu dotaz pro získání zvoleného vlákna příspěvků.
            
            ForumThreadDto currentForumThread = (forumThreadId > 0)
                ? await Mediator.Send(queryGetForumThread)
                : forumThreads.FirstOrDefault();
            
            // Sestavím model pro webové rozhraní.
            var model = HomeIndexViewModel.Create(currentForumThread, forumThreads);
            
            return View(model);
        }
    }
}
