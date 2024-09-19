using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace CofiApp.Domain.Files
{
    public class File : Entity, IAuditableEntity
    {
        public string Name { get; set; }
        public string ContainerName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        [NotMapped]
        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
