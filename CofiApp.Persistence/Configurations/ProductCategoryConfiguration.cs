using CofiApp.Domain.ProductCategories;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(TableNames.ProductCategories);

            builder.HasKey(pc => new { pc.ProductId, pc.MenuCategoryId });

            builder.HasOne(pc => pc.MenuCategory)
                .WithMany(mc => mc.ProductCategories)
                .HasForeignKey(pc => pc.MenuCategoryId);

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
        }
    }
}
