using CofiApp.Domain.ProductImageFiles;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class ProductImageFileConfiguration : IEntityTypeConfiguration<ProductImageFile>
    {
        public void Configure(EntityTypeBuilder<ProductImageFile> builder)
        {
            builder.ToTable(TableNames.ProductImageFiles);
        }
    }
}
