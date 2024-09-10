using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Users;
using CofiApp.Domain.UserVerificationTokens;

namespace CofiApp.Application.Authentication.Commands.EmailConfirmation
{
    public class EmailConfirmationCommandHandler : ICommandHandler<EmailConfirmationCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserVerificationTokenRepository _userVerificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmailConfirmationCommandHandler(IUserRepository userRepository, IUserVerificationTokenRepository userVerificationTokenRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userVerificationTokenRepository = userVerificationTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var maybeUserVerificationToken = await _userVerificationTokenRepository.GetByTokenAsync(request.Token);

            if (maybeUserVerificationToken.HasNoValue)
            {
                return Result.Failure(DomainErrors.Authentication.InvalidEmailConfirmationToken);
            }

            UserVerificationToken userVerificationToken = maybeUserVerificationToken.Value;

            if (userVerificationToken.TokenType.Equals(UserVerificationTokenType.EmailConfirm.ToString()) && (userVerificationToken.ExpiresOnUtc < DateTime.UtcNow || userVerificationToken.IsUsed))
            {
                return Result.Failure(DomainErrors.Authentication.InvalidEmailConfirmationToken);
            }

            var maybeUser = await _userRepository.GetByIdAsync(userVerificationToken.UserId);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User user = maybeUser.Value;

            if (user.EmailConfirmed)
            {
                return Result.Failure(DomainErrors.Authentication.EmailAlreadyConfirmed);
            }

            user.EmailConfirmed = true;

            _userRepository.Update(user);

            userVerificationToken.IsUsed = true;

            _userVerificationTokenRepository.Update(userVerificationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
