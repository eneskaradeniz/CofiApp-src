using CofiApp.Domain.Products;

namespace CofiApp.Domain.ProductImageFiles
{
    public class ProductImageFile : Files.File
    {
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
