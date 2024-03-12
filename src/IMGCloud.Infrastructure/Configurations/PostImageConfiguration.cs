using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMGCloud.Infrastructure.Configurations;

public sealed class PostImageConfiguration : IEntityTypeConfiguration<PostImage>
{
    public void Configure(EntityTypeBuilder<PostImage> builder)
    {
        builder.ToTable("PostImages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.HasOne(x => x.Post).WithMany(x => x.PostImages).HasForeignKey(x => x.PostId);
    }
}

