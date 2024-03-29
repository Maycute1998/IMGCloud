﻿using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMGCloud.Infrastructure.Configurations;

public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.Heart).HasDefaultValue(0);
        builder.Property(x => x.Caption).HasColumnType("nvarchar(max)");


    }
}
