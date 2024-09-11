using CofiApp.Domain.Baskets;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable(TableNames.Baskets);

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(b => b.CreatedOnUtc)
                .IsRequired();

            builder.Property(b => b.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Baskets)
                .HasForeignKey(b => b.UserId)
                .IsRequired();

            builder.HasQueryFilter(b => !b.User.Deleted);
        }
    }
}
