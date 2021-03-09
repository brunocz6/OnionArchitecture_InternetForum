using InternetForum.Application.Common.Interfaces;
using InternetForum.Domain.Repositories;
using InternetForum.Infrastructure.Identity;
using InternetForum.Infrastructure.Persistence;
using InternetForum.Infrastructure.Persistence.Repositories;
using InternetForum.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetForum.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Připojení k databázi (v paměti/k SQL serveru).
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("inMemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            // Přidání repozitářů.
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped(typeof(IForumThreadRepository), typeof(ForumThreadRepository));
            services.AddScoped(typeof(IPostRepository), typeof(PostRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));

            // Jednotka pro správu/manipulaci datového zdroje.
            services.AddScoped<IUnitOfWork>(provider => provider.GetService<ApplicationDbContext>());

            // Služba pro publish událostí na doménových entitách.
            services.AddScoped<IDomainEventService, DomainEventService>();

            // Služby pro autentizaci/správu uživatelů aplikace.
            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Služba pro zjištění aktuálního času.
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}
