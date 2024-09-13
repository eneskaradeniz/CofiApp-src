using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Emails;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Emails;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Users;
using CofiApp.Domain.UserVerificationTokens;

namespace CofiApp.Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, Result>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IUserVerificationTokenRepository _userVerificationTokenRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDateTime _dateTime;
        private readonly IUnitOfWork _unitOfWork;

        public ForgotPasswordCommandHandler(IEmailService emailService, IUserRepository userRepository, ITokenProvider tokenProvider, IUserVerificationTokenRepository userVerificationTokenRepository, IDateTime dateTime, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
            _userVerificationTokenRepository = userVerificationTokenRepository;
            _dateTime = dateTime;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            User user = maybeUser.Value;

            if (!user.EmailConfirmed)
            {
                return Result.Failure(DomainErrors.Authentication.EmailNotConfirmed);
            }

            var maybeUserVerificationToken = await _userVerificationTokenRepository.GetByUserIdAsync(user.Id, cancellationToken);

            if (maybeUserVerificationToken.HasNoValue)
            {
                return Result.Failure(DomainErrors.General.NotFound);
            }

            UserVerificationToken userVerificationToken = maybeUserVerificationToken.Value;

            if (userVerificationToken.TokenType.Equals(UserVerificationTokenType.PasswordReset.ToString()))
            {
                if (userVerificationToken.ExpiresOnUtc > _dateTime.UtcNow && !userVerificationToken.IsUsed)
                {
                    return Result.Failure(DomainErrors.Authentication.PasswordResetTokenStillValid);
                }
            }

            userVerificationToken.Token = _tokenProvider.CreateBase64Token();
            userVerificationToken.ExpiresOnUtc = _dateTime.UtcNow.AddHours(1);
            userVerificationToken.CreatedOnUtc = _dateTime.UtcNow;
            userVerificationToken.TokenType = UserVerificationTokenType.PasswordReset.ToString();
            userVerificationToken.IsUsed = false;

            _userVerificationTokenRepository.Update(userVerificationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var baseUrl = "https://myapp.com";
            var endpoint = "/reset-password";
            var url = $"{baseUrl}{endpoint}?token={userVerificationToken.Token}";

            MailRequest mailRequest = new()
            {
                EmailTo = user.Email,
                Subject = "Reset Password",
                Body = $"Click <a href='{url}'>here</a> to reset your password.",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(mailRequest);

            return Result.Success();
        }
    }
}
