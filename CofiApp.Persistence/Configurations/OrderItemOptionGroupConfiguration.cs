using CofiApp.Domain.OrderItemOptionGroups;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class OrderItemOptionGroupConfiguration : IEntityTypeConfiguration<OrderItemOptionGroup>
    {
        public void Configure(EntityTypeBuilder<OrderItemOptionGroup> builder)
        {
            builder.ToTable(TableNames.OrderItemOptionGroups);

            builder.HasKey(oio => oio.Id);

            builder.Property(oio => oio.ProductOptionGroupId)
                .IsRequired();

            builder.Property(oio => oio.ProductOptionGroupName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(oio => oio.ProductOptionGroupIsRequired)
                .IsRequired();

            builder.Property(oio => oio.ProductOptionGroupAllowMultiple)
                .IsRequired();

            builder.Property(oio => oio.CreatedOnUtc)
                .IsRequired();

            builder.Property(oio => oio.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(oio => oio.OrderItem)
                .WithMany(oi => oi.OrderItemOptionGroups)
                .HasForeignKey(oio => oio.OrderItemId)
                .IsRequired();

            builder.HasMany(oio => oio.OrderItemOptions)
                .WithOne(oio => oio.OrderItemOptionGroup)
                .HasForeignKey(oio => oio.OrderItemOptionGroupId)
                .IsRequired();

            builder.HasQueryFilter(oio => !oio.OrderItem.Order.User.Deleted);
        }
    }
}
