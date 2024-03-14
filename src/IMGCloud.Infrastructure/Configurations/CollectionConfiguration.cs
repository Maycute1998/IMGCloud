using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMGCloud.Infrastructure.Configurations;

public sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{

    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collections");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.CollectionName).HasColumnType("nvarchar(100)");


        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Collection)
            .HasForeignKey(x => x.CollectionId)
            .IsRequired(false);
    }
}