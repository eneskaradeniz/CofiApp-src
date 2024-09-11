namespace CofiApp.Contracts.ProductOptionGroups
{
    public class CreateProductOptionGroupRequest
    {
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
