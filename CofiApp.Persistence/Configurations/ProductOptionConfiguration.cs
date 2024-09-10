using CofiApp.Domain.ProductOptions;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.ToTable(TableNames.ProductOptions);

            builder.HasKey(po => po.Id);

            builder.Property(po => po.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(po => po.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(po => po.CreatedOnUtc)
                .IsRequired();

            builder.Property(po => po.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(po => po.ProductOptionGroup)
                .WithMany(pog => pog.ProductOptions)
                .HasForeignKey(po => po.ProductOptionGroupId)
                .IsRequired();
        }
    }
}
