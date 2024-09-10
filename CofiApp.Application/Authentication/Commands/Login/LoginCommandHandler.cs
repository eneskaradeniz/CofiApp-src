using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Cryptography;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.UserRefreshTokens;
using CofiApp.Domain.Users;

namespace CofiApp.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, Result<TokenResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokensRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDateTime _dateTime;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider jwtProvider, IDateTime dateTime, IUnitOfWork unitOfWork, IUserRefreshTokenRepository userRefreshTokensRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = jwtProvider;
            _dateTime = dateTime;
            _unitOfWork = unitOfWork;
            _userRefreshTokensRepository = userRefreshTokensRepository;
        }

        public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var maybeUser = await _userRepository.GetByEmailAsync(request.Email);

            if (maybeUser.HasNoValue)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            User user = maybeUser.Value;

            bool passwordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!passwordValid)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);
            }

            if (!user.EmailConfirmed)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.EmailNotConfirmed);
            }

            var accessToken = await _tokenProvider.CreateAccessTokenAsync(user.Id);
            var refreshToken = _tokenProvider.CreateBase64Token();
            TokenResponse tokenResponse = new(accessToken, refreshToken);

            var maybeUserRefreshToken = await _userRefreshTokensRepository.GetByUserIdAsync(user.Id);

            if (maybeUserRefreshToken.HasNoValue)
            {
                UserRefreshToken userRefreshToken = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Token = tokenResponse.RefreshToken,
                    ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
                    CreatedOnUtc = DateTime.UtcNow
                };

                _userRefreshTokensRepository.Insert(userRefreshToken);
            } else
            {
                UserRefreshToken userRefreshToken = maybeUserRefreshToken.Value;

                userRefreshToken.Token = tokenResponse.RefreshToken;
                userRefreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);
                userRefreshToken.CreatedOnUtc = DateTime.UtcNow;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(tokenResponse);
        }
    }
}
