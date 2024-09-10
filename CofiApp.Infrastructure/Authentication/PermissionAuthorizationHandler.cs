using CofiApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CofiApp.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var permissions = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaimTypes.Permissions)
                .Select(x => x.Value)
                .ToHashSet();

            if (permissions.Contains(Permission.All.ToString()) || 
                permissions.Contains(requirement.Permission.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
