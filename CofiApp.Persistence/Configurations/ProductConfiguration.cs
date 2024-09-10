using CofiApp.Domain.Products;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(TableNames.Products);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Deleted)
                .HasDefaultValue(false);

            builder.Property(p => p.DeletedOnUtc)
                .IsRequired(false);

            builder.Property(p => p.CreatedOnUtc)
                .IsRequired();

            builder.Property(p => p.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasMany(p => p.ProductOptionGroups)
                .WithOne(pog => pog.Product)
                .HasForeignKey(pog => pog.ProductId);

            builder.HasQueryFilter(p => !p.Deleted);
        }
    }
}
