using InternetForum.Domain.Entities;
using InternetForum.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetForum.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            
            builder.Property(e => e.Body)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.AuthorId)
                .IsRequired();
        }
    }
}
