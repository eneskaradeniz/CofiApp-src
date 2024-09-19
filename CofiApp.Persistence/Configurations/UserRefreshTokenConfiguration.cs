using CofiApp.Domain.UserRefreshTokens;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    internal sealed class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable(TableNames.UserRefreshTokens);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .IsRequired();

            builder.Property(x => x.ExpiresOnUtc)
                .IsRequired();

            builder.Property(x => x.CreatedOnUtc)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.UserRefreshToken)
                .HasForeignKey<UserRefreshToken>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(x => !x.User.Deleted);
        }
    }
}
