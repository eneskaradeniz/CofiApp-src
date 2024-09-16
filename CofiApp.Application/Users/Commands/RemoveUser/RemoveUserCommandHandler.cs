using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Users;

namespace CofiApp.Application.Users.Commands.RemoveUser
{
    public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User user = maybeUser.Value;

            _userRepository.Remove(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
