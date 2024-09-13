using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Messaging;
using CofiApp.Contracts.Authentication;
using CofiApp.Domain.Core.Errors;
using CofiApp.Domain.Core.Primitives.Result;
using CofiApp.Domain.UserRefreshTokens;

namespace CofiApp.Application.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Result<TokenResponse>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IDateTime _dateTime;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenCommandHandler(ITokenProvider tokenProvider, IUserRefreshTokenRepository userRefreshTokenRepository, IDateTime dateTime, IUnitOfWork unitOfWork)
        {
            _tokenProvider = tokenProvider;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _dateTime = dateTime;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var maybeUserRefreshToken = await _userRefreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);

            if (maybeUserRefreshToken.HasNoValue)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.InvalidRefreshToken);
            }

            UserRefreshToken userRefreshToken = maybeUserRefreshToken.Value;

            if (userRefreshToken.ExpiresOnUtc < _dateTime.UtcNow)
            {
                return Result.Failure<TokenResponse>(DomainErrors.Authentication.RefreshTokenExpired);
            }

            var accessToken = await _tokenProvider.CreateAccessTokenAsync(userRefreshToken.UserId);
            var refreshToken = _tokenProvider.CreateBase64Token();
            TokenResponse tokenResponse = new(accessToken, refreshToken);

            userRefreshToken.Token = tokenResponse.RefreshToken;
            userRefreshToken.ExpiresOnUtc = _dateTime.UtcNow.AddDays(7);
            userRefreshToken.CreatedOnUtc = _dateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(tokenResponse);
        }
    }
}
