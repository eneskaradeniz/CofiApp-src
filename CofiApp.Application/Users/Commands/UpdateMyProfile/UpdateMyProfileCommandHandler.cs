using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Users;

namespace CofiApp.Application.Users.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommandHandler : ICommandHandler<UpdateMyProfileCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UpdateMyProfileCommandHandler(IUserIdentifierProvider userIdentifierProvider, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(UpdateMyProfileCommand request, CancellationToken cancellationToken)
        {
            if (_userIdentifierProvider.UserId == Guid.Empty)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            var maybeUser = await _userRepository.GetByIdAsync(_userIdentifierProvider.UserId, cancellationToken);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User user = maybeUser.Value;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
