using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Primitives.Result;

namespace CofiApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : ICommand<Result>
    {
        public UpdateUserCommand(Guid userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
