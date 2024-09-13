using CofiApp.Domain.BasketItemOptionGroups;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class BasketItemOptionGroupConfiguration : IEntityTypeConfiguration<BasketItemOptionGroup>
    {
        public void Configure(EntityTypeBuilder<BasketItemOptionGroup> builder)
        {
            builder.ToTable(TableNames.BasketItemOptionGroups);

            builder.HasKey(biog => biog.Id);

            builder.Property(biog => biog.CreatedOnUtc)
                .IsRequired();

            builder.Property(biog => biog.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(biog => biog.BasketItem)
                .WithMany(bi => bi.BasketItemOptionGroups)
                .HasForeignKey(biog => biog.BasketItemId)
                .IsRequired();

            builder.HasOne(biog => biog.ProductOptionGroup)
                .WithMany(pog => pog.BasketItemOptionGroups)
                .HasForeignKey(biog => biog.ProductOptionGroupId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasQueryFilter(biog => !biog.BasketItem.Basket.User.Deleted);
        }
    }
}
