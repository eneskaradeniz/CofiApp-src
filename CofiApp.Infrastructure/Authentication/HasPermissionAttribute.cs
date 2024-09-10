using CofiApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CofiApp.Infrastructure.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            : base(policy: permission.ToString())
        {

        }
    }
}
