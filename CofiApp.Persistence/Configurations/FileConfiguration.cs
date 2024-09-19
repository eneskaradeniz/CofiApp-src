using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Files.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Files.File> builder)
        {
            builder.ToTable(TableNames.Files);

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.ContainerName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.ContentType)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.Size)
                .IsRequired()
                .HasColumnType("bigint");

            builder.Property(f => f.CreatedOnUtc)
                .IsRequired();

            builder.Ignore(f => f.ModifiedOnUtc);
        }
    }
}
