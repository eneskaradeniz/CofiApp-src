using CofiApp.Application.Abstractions.Authentication;
using CofiApp.Application.Abstractions.Common;
using CofiApp.Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CofiApp.Infrastructure.Authentication
{
    internal class TokenProvider : ITokenProvider
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTime _dateTime;
        private readonly IPermissionService _permissionService;

        public TokenProvider(IOptions<JwtSettings> jwtOptions, IDateTime dateTime, IPermissionService permissionService)
        {
            _jwtSettings = jwtOptions.Value;
            _dateTime = dateTime;
            _permissionService = permissionService;
        }

        public async Task<string> CreateAccessTokenAsync(Guid userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString())
            };

            HashSet<string> permissions = await _permissionService
                .GetPermissionsAsync(userId);

            foreach (string permission in permissions)
            {
                claims.Add(new(CustomClaimTypes.Permissions, permission));
            }

            DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                tokenExpirationTime,
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public string CreateBase64Token()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
