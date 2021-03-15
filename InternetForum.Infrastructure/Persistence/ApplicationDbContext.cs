using InternetForum.Application.Common.Interfaces;
using InternetForum.Domain.Common;
using InternetForum.Domain.Interfaces;
using InternetForum.Domain.Repositories;
using InternetForum.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using InternetForum.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InternetForum.Infrastructure.Persistence
{
    /// <summary>
    /// Třída databázového kontextu (jednotky pro správu dat v databázi).
    /// </summary>
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IUnitOfWork
    {
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IDomainEventService domainEventService,
            IDateTime dateTime,
            ICurrentUserService currentUserService) : base(options, operationalStoreOptions)
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ForumThread> ForumThreads { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ForumThreadUser> ForumThreadUsers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                var entry = (EntityEntry<IAuditableEntity>) entityEntry;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity is null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
