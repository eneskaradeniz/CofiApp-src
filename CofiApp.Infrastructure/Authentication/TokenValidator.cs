using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Domain.Core.Primitives.Maybe;
using CofiApp.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CofiApp.Infrastructure.Authentication
{
    public class TokenValidator : ITokenValidator
    {
        private readonly JwtSettings _jwtSettings;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public TokenValidator(IOptions<JwtSettings> jwtSettings, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSettings = jwtSettings.Value;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        public Maybe<Guid> ValidateEmailVerifyToken(string token)
        {
            var securityKey = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    string? userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    return Guid.Parse(userId);
                }

                return Maybe<Guid>.None;
            } catch
            {
                return Maybe<Guid>.None;
            }
        }
    }
}
