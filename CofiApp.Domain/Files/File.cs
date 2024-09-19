using CofiApp.Domain.Core.Abstractions;
using CofiApp.Domain.Core.Primitives;
using CofiApp.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CofiApp.Domain.Files
{
    public class File : Entity, IAuditableEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public StorageType StorageType { get; set; }
        
        public DateTime CreatedOnUtc { get; set; }
        [NotMapped]
        public DateTime? ModifiedOnUtc { get; private set; }
    }
}
