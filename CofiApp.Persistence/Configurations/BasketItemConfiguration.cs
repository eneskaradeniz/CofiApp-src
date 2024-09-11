using CofiApp.Domain.BasketItems;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable(TableNames.BasketItems);

            builder.HasKey(bi => bi.Id);

            builder.Property(bi => bi.Quantity)
                .IsRequired();

            builder.Property(bi => bi.CreatedOnUtc)
                .IsRequired();

            builder.Property(bi => bi.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(bi => bi.Basket)
                .WithMany(b => b.BasketItems)
                .HasForeignKey(bi => bi.BasketId)
                .IsRequired();

            builder.HasOne(bi => bi.Product)
                .WithMany()
                .HasForeignKey(bi => bi.ProductId)
                .IsRequired();       
            
            builder.HasQueryFilter(bi => !bi.Basket.User.Deleted);
        }
    }
}
