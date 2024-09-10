using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Authentication.Commands.Login
{
    public class LoginCommand : ICommand<Result<TokenResponse>>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }
}
