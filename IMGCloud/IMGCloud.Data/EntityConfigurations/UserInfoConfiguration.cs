using IMGCloud.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMGCloud.Data.EntityConfigurations
{
    internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable("UserInfos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.FullName).HasMaxLength(200);
            builder.Property(x => x.Photo).HasColumnType("nvarchar(MAX)");
        }
    }
}
