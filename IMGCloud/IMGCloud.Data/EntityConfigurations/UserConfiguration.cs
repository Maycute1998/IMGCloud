using IMGCloud.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMGCloud.Data.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(u => u.UserName).IsUnicode(false).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Password).IsUnicode(false).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Email).IsUnicode(false).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.UserInfos)
                .WithOne(x => x.User)
                .HasForeignKey<UserInfo>(x => x.UserId)
                .IsRequired();

            builder.HasOne(x => x.UserTokens)
                .WithOne(x => x.User)
                .HasForeignKey<UserToken>(x => x.UserId)
                .IsRequired();
        }
    }
}
