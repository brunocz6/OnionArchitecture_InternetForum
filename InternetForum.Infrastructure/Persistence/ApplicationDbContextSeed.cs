using InternetForum.Domain.Entities;
using InternetForum.Domain.ValueObjects;
using InternetForum.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InternetForum.Infrastructure.Persistence
{
    /// <summary>
    /// Třída obsahující inicializaci výchozího stavu databáze.
    /// </summary>
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "admin", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Admin123!");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async Task SeedDefaultDataAsync(ApplicationDbContext context)
        {
            // Vytvoření hlavního vlákna příspěvků.
            var mainForumThread = new ForumThread()
            {
                Name = "Hlavní vlákno",
                Description = "Toto je hlavní vlákno internetového fóra.",
                CreatedAt = DateTime.Now 
            };

            var isMainThreadInDatabase = await context.ForumThreads.AnyAsync(ft => ft.Name == mainForumThread.Name);

            if (!isMainThreadInDatabase)
            {
                await context.AddAsync(mainForumThread);
            }

            // Uložení změn do databáze.
            await context.SaveChangesAsync();
        }
    }
}
