using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using IMGCloud.Domain.Entities;

namespace IMGCloud.Infrastructure.Configurations;

public sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{

    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collection");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.CollectionName).HasColumnType("nvarchar(100)");


        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Collection)
            .HasForeignKey(x => x.CollectionId)
            .IsRequired(false);
    }
}