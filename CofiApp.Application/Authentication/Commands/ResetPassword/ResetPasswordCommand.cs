using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommand : ICommand<Result>
    {
        public ResetPasswordCommand(string token, string password, string confirmPassword)
        {
            Token = token;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Token { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }
}
