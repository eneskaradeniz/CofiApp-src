using CofiApp.Domain.Authentication;
using CofiApp.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CofiApp.Persistence.Configurations
{
    internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableNames.Permissions);

            builder.HasKey(x => x.Id);

            IEnumerable<Permission> permissions = Enum
                .GetValues<Domain.Enums.Permission>()
                .Select(x => new Permission()
                {
                    Id = (int)x,
                    Name = x.ToString()
                });

            builder.HasData(permissions);
        }
    }
}
