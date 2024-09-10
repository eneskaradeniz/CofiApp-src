using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : ICommand<Result>
    {
        public ForgotPasswordCommand(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
