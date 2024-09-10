using CofiApp.Domain.OrderItemOptions;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class OrderItemOptionConfiguration : IEntityTypeConfiguration<OrderItemOption>
    {
        public void Configure(EntityTypeBuilder<OrderItemOption> builder)
        {
            builder.ToTable(TableNames.OrderItemOptions);

            builder.HasKey(oio => oio.Id);

            builder.HasOne(oio => oio.OrderItem)
                .WithMany(oi => oi.OrderItemOptions)
                .HasForeignKey(oio => oio.OrderItemId)
                .IsRequired();

            builder.Property(oio => oio.ProductOptionName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(oio => oio.ProductOptionPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(oio => oio.CreatedOnUtc)
                .IsRequired();

            builder.Property(oio => oio.ModifiedOnUtc)
                .IsRequired(false);
        }
    }
}
