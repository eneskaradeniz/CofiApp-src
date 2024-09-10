namespace CofiApp.Application.Abstractions.Authentication
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(Guid userId);
    }
}
