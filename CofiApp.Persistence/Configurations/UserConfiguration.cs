using CofiApp.Domain.Authentication;
using CofiApp.Domain.Users;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.CreatedOnUtc)
                .IsRequired();

            builder.Property(u => u.ModifiedOnUtc)
                .IsRequired(false);

            builder.Property(x => x.DeletedOnUtc)
                .IsRequired(false);

            builder.Property(x => x.Deleted)
                .HasDefaultValue(false);

            builder.HasMany(u => u.Baskets)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Roles)
                .WithMany()
                .UsingEntity<UserRole>();

            builder
                .HasOne(x => x.UserRefreshToken)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.Id);

            builder
                .HasOne(x => x.UserVerificationToken)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.Id);

            builder.HasQueryFilter(u => !u.Deleted);
        }
    }
}
