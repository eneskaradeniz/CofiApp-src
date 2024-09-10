using CofiApp.Domain.BasketItemOptions;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class BasketItemOptionConfiguration : IEntityTypeConfiguration<BasketItemOption>
    {
        public void Configure(EntityTypeBuilder<BasketItemOption> builder)
        {
            builder.ToTable(TableNames.BasketItemOptions);

            builder.HasKey(bio => bio.Id);

            builder.HasOne(bio => bio.BasketItem)
                .WithMany(bi => bi.BasketItemOptions)
                .HasForeignKey(bio => bio.BasketItemId)
                .IsRequired();

            builder.HasOne(bio => bio.ProductOption)
                .WithMany()
                .HasForeignKey(bio => bio.ProductOptionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Property(po => po.CreatedOnUtc)
                .IsRequired();

            builder.Property(po => po.ModifiedOnUtc)
                .IsRequired(false);
        }
    }
}
