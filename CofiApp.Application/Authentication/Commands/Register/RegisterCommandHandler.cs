using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Emails;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Contracts.Emails;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.Enums;
using CofiApp.Domain.Users;
using CofiApp.Domain.UserVerificationTokens;

namespace CofiApp.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserVerificationTokenRepository _userVerificationTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ITokenProvider _tokenProvider;

        public RegisterCommandHandler(IUserRepository userRepository, IUserVerificationTokenRepository userVerificationTokenRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IEmailService emailService, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _userVerificationTokenRepository = userVerificationTokenRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
            {
                return Result.Failure<TokenResponse>(DomainErrors.User.DuplicateEmail);
            }

            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
            };

            _userRepository.Insert(user);

            UserVerificationToken userVerificationToken = new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenType = UserVerificationTokenType.EmailConfirm.ToString(),
                Token = _tokenProvider.CreateBase64Token(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(1),
                CreatedOnUtc = DateTime.UtcNow,
                IsUsed = false
            };

            _userVerificationTokenRepository.Insert(userVerificationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // TODO: Move to configuration
            var baseUrl = "https://localhost:5001";
            var endpoint = "/api/auth/email-confirmation";
            var link = $"{baseUrl}{endpoint}?token={userVerificationToken.Token}";

            MailRequest mailRequest = new()
            {
                EmailTo = user.Email,
                Subject = "Email verification",
                Body = $"Please verify your email by clicking <a href='{link}'>here</a>",
                IsHtml = true
            };

            await _emailService.SendEmailAsync(mailRequest);

            return Result.Success();
        }
    }
}
