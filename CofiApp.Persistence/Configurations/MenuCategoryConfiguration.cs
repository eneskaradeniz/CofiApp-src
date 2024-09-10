using CofiApp.Domain.MenuCategories;
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

            builder.Property(mc => mc.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(mc => mc.CreatedOnUtc)
                .IsRequired();

            builder.Property(mc => mc.ModifiedOnUtc)
                .IsRequired(false);
        }
    }
}
