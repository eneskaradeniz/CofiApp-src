using CofiApp.Domain.Core.Primitives;

namespace CofiApp.Domain.Core.Errors
{
    public static class DomainErrors
    {
        public static class General
        {
            public static Error UnProcessableRequest => new(
                "General.UnProcessableRequest",
                "The server could not process the request.");

            public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");

            public static Error NotFound => new("General.NotFound", "The requested resource was not found.");

            public static Error AlreadyExists => new("General.AlreadyExists", "The specified resource already exists.");
        }

        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new(
                "Authentication.InvalidEmailOrPassword",
                "The specified email or password are incorrect.");

            public static Error EmailNotConfirmed => new("Authentication.EmailNotConfirmed", "Email is not confirmed.");

            public static Error DuplicateName => new("Authentication.DuplicateName", "The specified name is already in use.");

            public static Error InvalidRefreshToken => new("Authentication.InvalidRefreshToken", "Invalid refresh token.");

            public static Error RefreshTokenExpired => new("Authentication.RefreshTokenExpired", "Refresh token has expired.");

            public static Error InvalidEmailConfirmationToken => new("Authentication.InvalidEmailConfirmationToken", "Invalid email confirmation token.");

            public static Error InvalidPasswordResetToken => new("Authentication.InvalidPasswordResetToken", "Invalid password reset token.");

            public static Error EmailAlreadyConfirmed => new("Authentication.EmailAlreadyConfirmed", "Email is already confirmed.");

            public static Error PasswordResetTokenStillValid => new("Authentication.PasswordResetTokenStillValid", "Password reset token is still valid.");

            public static Error PasswordSameAsOld => new("Authentication.PasswordSameAsOld", "New password cannot be the same as the old password.");
        }

        public static class User
        {
            public static Error InvalidId => new("User.InvalidId", "Invalid user id");
            public static Error DuplicateEmail => new("User.DuplicateEmail", "The specified email is already in use.");
            public static Error EmailAlreadyExists => new("User.EmailAlreadyExists", "The specified email is already in use.");
        }

        public static class MenuCategory
        {
            public static Error DuplicateName => new("MenuCategory.DuplicateName", "The specified name is already in use.");
        }
    }
}
