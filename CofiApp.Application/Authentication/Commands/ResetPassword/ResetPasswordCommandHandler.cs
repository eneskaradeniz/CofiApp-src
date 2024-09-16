using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Users;
using CofiApp.Domain.UserVerificationTokens;

namespace CofiApp.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IUserVerificationTokenRepository _userVerificationTokenRepository;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IUserRepository userRepository, IUserVerificationTokenRepository userVerificationTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _userVerificationTokenRepository = userVerificationTokenRepository;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var maybeUserVerificationToken = await _userVerificationTokenRepository.GetByTokenAsync(request.Token, cancellationToken);

            if (maybeUserVerificationToken.HasNoValue)
            {
                return Result.Failure(DomainErrors.Authentication.InvalidPasswordResetToken);
            }

            UserVerificationToken userVerificationToken = maybeUserVerificationToken.Value;

            if (userVerificationToken.TokenType.Equals(UserVerificationTokenType.PasswordReset.ToString()) && (userVerificationToken.ExpiresOnUtc < DateTime.UtcNow || userVerificationToken.IsUsed))
            {
                return Result.Failure(DomainErrors.Authentication.InvalidPasswordResetToken);
            }

            var maybeUser = await _userRepository.GetByIdAsync(userVerificationToken.UserId, cancellationToken);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User user = maybeUser.Value;

            if (_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return Result.Failure(DomainErrors.Authentication.PasswordSameAsOld);
            }

            user.PasswordHash = _passwordHasher.Hash(request.Password);

            _userRepository.Update(user);

            userVerificationToken.IsUsed = true;

            _userVerificationTokenRepository.Update(userVerificationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
