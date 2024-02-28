using IMGCloud.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.EntityConfigurations
{
    public class PostCollectionConfiguration : IEntityTypeConfiguration<PostCollection>
    {

        public void Configure(EntityTypeBuilder<PostCollection> builder)
        {
            builder.ToTable("PostCollections");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CollectionName).HasColumnType("nvarchar(100)");


            builder.HasMany(x => x.Posts)
                .WithOne(x => x.PostCollections)
                .HasForeignKey(x => x.CollectionId)
                .IsRequired(false); ;
        }
    }
}
