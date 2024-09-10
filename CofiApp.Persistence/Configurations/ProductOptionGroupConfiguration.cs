using CofiApp.Domain.ProductOptionGroups;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductOptionGroupConfiguration : IEntityTypeConfiguration<ProductOptionGroup>
    {
        public void Configure(EntityTypeBuilder<ProductOptionGroup> builder)
        {
            builder.ToTable(TableNames.ProductOptionGroups);

            builder.HasKey(pog => pog.Id);

            builder.Property(pog => pog.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pog => pog.IsRequired)
                .HasDefaultValue(false);

            builder.Property(pog => pog.AllowMultiple)
                .HasDefaultValue(false);

            builder.Property(pog => pog.CreatedOnUtc)
                .IsRequired();

            builder.Property(pog => pog.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(pog => pog.Product)
                .WithMany(p => p.ProductOptionGroups)
                .HasForeignKey(pog => pog.ProductId)
                .IsRequired();

            builder.HasMany(pog => pog.ProductOptions)
                .WithOne(po => po.ProductOptionGroup)
                .HasForeignKey(po => po.ProductOptionGroupId);
        }
    }
}
