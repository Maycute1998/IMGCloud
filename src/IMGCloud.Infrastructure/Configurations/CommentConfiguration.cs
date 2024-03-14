using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.Infrastructure.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comment");
        builder.Property(x => x.Content).IsRequired().HasMaxLength(200);

        builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
    }
}
