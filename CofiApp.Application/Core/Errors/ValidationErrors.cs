using CofiApp.Domain.Core.Primitives;

namespace CofiApp.Application.Core.Errors
{
    internal static class ValidationErrors
    {
        internal static class Users
        {
            internal static Error UserIdIsRequired = new("Authentication.UserIdIsRequired", "The user id is required.");

            internal static Error FirstNameIsRequired = new("Authentication.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired = new("Authentication.LastNameIsRequired", "The last name is required.");
        }

        internal static class Authentication
        {
            internal static Error UserIdIsRequired = new("Authentication.UserIdIsRequired", "The user id is required.");

            internal static Error FirstNameIsRequired = new("Authentication.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired = new("Authentication.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired = new("Authentication.EmailIsRequired", "The email is required.");

            internal static Error EmailIsInvalid = new("Authentication.EmailIsInvalid", "The email is invalid.");

            internal static Error PasswordIsRequired = new("Authentication.PasswordIsRequired", "The password is required.");

            internal static Error PasswordIsTooShort = new("Authentication.PasswordIsTooShort", "The password is too short.");

            internal static Error ConfirmPasswordIsRequired = new("Authentication.ConfirmPasswordIsRequired", "The confirm password is required.");

            internal static Error ConfirmPasswordIsDifferent = new("Authentication.ConfirmPasswordIsDifferent", "The confirm password is different from the password.");

            internal static Error RefreshTokenIsRequired = new("Authentication.RefreshTokenIsRequired", "The refresh token is required.");

            internal static Error TokenIsRequired = new("Authentication.TokenIsRequired", "The token is required.");
        }

        internal static class Roles
        {
            internal static Error RoleIdIsRequired = new("Roles.RoleIdIsRequired", "The role id is required.");

            internal static Error PermissionIdIsRequired = new("Roles.PermissionIdIsRequired", "The permission id is required.");

            internal static Error UserIdIsRequired = new("Roles.UserIdIsRequired", "The user id is required.");

            internal static Error NameIsRequired = new("Roles.NameIsRequired", "The name is required.");

            internal static Error NameIsTooLong = new("Roles.NameTooLong", "The name is too long.");
        }
    }
}
