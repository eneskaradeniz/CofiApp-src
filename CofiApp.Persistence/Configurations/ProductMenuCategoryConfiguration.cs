using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductMenuCategoryConfiguration : IEntityTypeConfiguration<ProductMenuCategory>
    {
        public void Configure(EntityTypeBuilder<ProductMenuCategory> builder)
        {
            builder.ToTable(TableNames.ProductMenuCategories);

            builder.HasKey(pc => new { pc.ProductId, pc.MenuCategoryId });

            builder.HasOne(pc => pc.MenuCategory)
                .WithMany(mc => mc.ProductMenuCategories)
                .HasForeignKey(pc => pc.MenuCategoryId);

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductMenuCategories)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasQueryFilter(pc => !pc.Product.Deleted);
        }
    }
}
