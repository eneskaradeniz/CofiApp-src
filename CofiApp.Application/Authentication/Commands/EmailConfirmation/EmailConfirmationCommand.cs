using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Authentication.Commands.EmailConfirmation
{
    public class EmailConfirmationCommand : ICommand<Result>
    {
        public EmailConfirmationCommand(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
