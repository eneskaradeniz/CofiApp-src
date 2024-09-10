using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Authentication.Commands.Register
{
    public class RegisterCommand : ICommand<Result>
    {
        public RegisterCommand(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }
}
