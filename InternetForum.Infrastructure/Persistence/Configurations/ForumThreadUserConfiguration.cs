using InternetForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetForum.Infrastructure.Persistence.Configurations
{
    public class ForumThreadUserConfiguration : IEntityTypeConfiguration<ForumThreadUser>
    {
        public void Configure(EntityTypeBuilder<ForumThreadUser> builder)
        {
            builder.ToTable("ForumThreadUsers");
            
            builder.Property(e => e.UserId)
                .IsRequired();
        }
    }
}