using CofiApp.Domain.UserVerificationTokens;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class UserVerificationTokenConfiguration : IEntityTypeConfiguration<UserVerificationToken>
    {
        public void Configure(EntityTypeBuilder<UserVerificationToken> builder)
        {
            builder.ToTable(TableNames.UserVerificationTokens);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.ExpiresOnUtc).IsRequired();
            builder.Property(x => x.CreatedOnUtc).IsRequired();
            builder.Property(x => x.TokenType).IsRequired();
            builder.Property(x => x.IsUsed).IsRequired();

            builder.HasQueryFilter(x => !x.User.Deleted);

            builder
                 .HasOne(x => x.User)
                 .WithOne(x => x.UserVerificationToken)
                 .HasForeignKey<UserVerificationToken>(x => x.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
