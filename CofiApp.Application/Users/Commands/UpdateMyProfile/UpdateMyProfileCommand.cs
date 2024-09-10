using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Users.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommand : ICommand<Result>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public UpdateMyProfileCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
