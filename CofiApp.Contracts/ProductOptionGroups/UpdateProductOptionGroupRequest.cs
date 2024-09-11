namespace CofiApp.Contracts.ProductOptionGroups
{
    public class UpdateProductOptionGroupRequest
    {
        public required string Name { get; set; }
        public bool IsRequired { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
