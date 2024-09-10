namespace CofiApp.Contracts.Authentication
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public List<PermissionResponse>? Permissions { get; set; }
    }
}
