using CofiApp.Domain.MenuCategories;
using CofiApp.Domain.ProductMenuCategories;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class MenuCategoryConfiguration : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.ToTable(TableNames.MenuCategories);

            builder.HasKey(mc => mc.Id);

            builder.HasIndex(mc => mc.Name)
                .IsUnique();

            builder.Property(mc => mc.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(mc => mc.CreatedOnUtc)
                .IsRequired();

            builder.Property(mc => mc.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasMany(mc => mc.Products)
                .WithMany(p => p.MenuCategories)
                .UsingEntity<ProductMenuCategory>(
                    j => j
                        .HasOne(pm => pm.Product)
                        .WithMany(p => p.ProductMenuCategories)
                        .HasForeignKey(pm => pm.ProductId)
                        .OnDelete(DeleteBehavior.Cascade),  // Cascade delete for Product
                    j => j
                        .HasOne(pm => pm.MenuCategory)
                        .WithMany(mc => mc.ProductMenuCategories)
                        .HasForeignKey(pm => pm.MenuCategoryId)
                        .OnDelete(DeleteBehavior.Cascade),  // Cascade delete for MenuCategory
                    j =>
                    {
                        j.HasKey(pm => new { pm.ProductId, pm.MenuCategoryId });
                    });
        }
    }
}
