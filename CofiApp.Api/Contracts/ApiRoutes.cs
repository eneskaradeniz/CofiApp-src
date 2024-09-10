namespace CofiApp.Api.Contracts
{
    public static class ApiRoutes
    {
        public static class Authentication
        {
            public const string Login = "auth/login";

            public const string Register = "auth/register";

            public const string RefreshToken = "auth/refresh-token";

            public const string ForgotPassword = "auth/forgot-password";

            public const string ResetPassword = "auth/reset-password";

            public const string EmailConfirmation = "auth/email-confirmation";
        }

        public static class Users
        {
            public const string Get = "users";

            public const string GetById = "users/{userId:guid}";

            public const string Create = "users";

            public const string Update = "users/{userId:guid}";

            public const string Remove = "users/{userId:guid}";

            public const string ChangePassword = "users/{userId:guid}/change-password";

            public const string UpdateMyProfile = "users/my-profile";

            public const string GetMyProfile = "users/my-profile";
        }

        public static class Roles
        {
            public const string Get = "roles";

            public const string Create = "roles";

            public const string Update = "roles/{roleId:guid}";

            public const string Remove = "roles/{roleId:guid}";

            public const string GetPermissions = "roles/permissions";

            public const string AssignPermission = "roles/{roleId:guid}/permissions/{permissionId}";

            public const string AssignUser = "roles/{roleId:guid}/users/{userId:guid}";
        }
    }
}
