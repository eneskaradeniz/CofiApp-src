using Microsoft.AspNetCore.Authorization;

namespace CofiApp.Infrastructure.Authentication
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
